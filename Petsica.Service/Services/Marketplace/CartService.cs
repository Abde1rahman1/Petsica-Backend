using Petsica.Core.Entities.Marketplace;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Contracts.Marketplace.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Services.Marketplace
{
    public class CartService(ApplicationDbContext context) : ICartService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result> AddToCartAsync(string userId, AddToCartRequest request, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);
            if (product == null || product.IsDeleted || !product.IsAvailable)
                return Result.Failure(MarketErrors.NoProduct);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null)
            {
                cart = new Cart { UserID = userId };
                await _context.Carts.AddAsync(cart, cancellationToken);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            var newQuantity = request.Quantity + (cartItem?.Quantity ?? 0);

            if (newQuantity > product.Quantity)
                return Result.Failure(MarketErrors.ExceedAvailableStock);

            if (cartItem != null)
                cartItem.Quantity = newQuantity;
            else
                cart.CartItems.Add(new CartItem { ProductId = request.ProductId, Quantity = request.Quantity });

            cart.TotalPrice = await CalculateTotalPrice(cart.CartItems, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        public async Task<Result<CartResponse>> GetCartItemsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null || !cart.CartItems.Any())
                return Result.Success(new CartResponse(new List<CartItemResponse>(), 0, 0));

            var items = cart.CartItems.Select(ci => new CartItemResponse(
                ProductId: ci.ProductId,
                ProductName: ci.Product.Name,
                Photo: ci.Product.Photo,
                Price: ci.Product.Price,
                Discount: ci.Product.Discount,
                Quantity: ci.Quantity,
                IsAvailable: !ci.Product.IsDeleted && ci.Product.IsAvailable,
                SubTotal: (ci.Product.Price - ci.Product.Discount) * ci.Quantity
            )).ToList();

            var totalQuantity = items.Sum(i => i.Quantity);
            var totalPrice = items.Sum(i => i.SubTotal);

            var response = new CartResponse(items, totalQuantity, totalPrice);
            return Result.Success(response);
        }


        public async Task<Result> RemoveFromCartAsync(string userId, int productId, CancellationToken cancellationToken = default)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null)
                return Result.Failure(MarketErrors.CartNotFound);

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (item == null)
                return Result.Failure(MarketErrors.CartItemNotFound);

            cart.CartItems.Remove(item);
            _context.CartItems.Remove(item);

            cart.TotalPrice = cart.CartItems.Any()
                ? await CalculateTotalPrice(cart.CartItems, cancellationToken)
                : 0;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        public async Task<Result> UpdateCartItemAsync(string userId, UpdateCartItemRequest request, CancellationToken cancellationToken = default)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null)
                return Result.Failure(MarketErrors.CartNotFound);

            var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            if (item == null)
                return Result.Failure(MarketErrors.CartItemNotFound);

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductID == request.ProductId && !p.IsDeleted && p.IsAvailable, cancellationToken);

            if (product == null)
                return Result.Failure(MarketErrors.NoProduct);

            if (request.Quantity > product.Quantity)
                return Result.Failure(MarketErrors.ExceedAvailableStock);

            if (request.Quantity == 0)
            {
                cart.CartItems.Remove(item);
                _context.CartItems.Remove(item);
            }
            else
            {
                item.Quantity = request.Quantity;
            }

            cart.TotalPrice = cart.CartItems.Any()
                ? await CalculateTotalPrice(cart.CartItems, cancellationToken)
                : 0;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }



        public async Task<Result> ClearCartAsync(string userId, CancellationToken cancellationToken = default)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null)
                return Result.Failure(MarketErrors.CartNotFound);

            if (cart.CartItems.Any())
            {
                _context.CartItems.RemoveRange(cart.CartItems); 
                cart.CartItems.Clear();
            }

            cart.TotalPrice = 0;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        private async Task<decimal> CalculateTotalPrice(List<CartItem> items, CancellationToken cancellationToken)
        {
            var productIds = items.Select(i => i.ProductId).ToList();
            var products = await _context.Products
                .Where(p => productIds.Contains(p.ProductID))
                .ToDictionaryAsync(p => p.ProductID, cancellationToken);

            return items.Sum(ci => (products[ci.ProductId].Price - products[ci.ProductId].Discount) * ci.Quantity);
        }
    }
}
