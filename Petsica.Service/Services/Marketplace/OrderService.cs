using Microsoft.AspNetCore.Http.HttpResults;
using Petsica.Core.Entities.Marketplace;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Shared.Contracts.Marketplace.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Services.Marketplace
{
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<List<OrderResponse>>> CreateOrderFromCartAsync(string userId, string address, CancellationToken cancellationToken)
        {
            // جلب الكارت الخاص باليوزر
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null || !cart.CartItems.Any())
                return Result.Failure<List<OrderResponse>>(MarketErrors.CartNotFound); // استخدام Result.Failure مع الخطأ المناسب

            // تجميع المنتجات حسب الـ Seller
            var groupedItems = cart.CartItems
                .GroupBy(ci => ci.Product.SellerID)
                .Select(g => new
                {
                    SellerId = g.Key,
                    Items = g.ToList()
                })
                .ToList();

            List<OrderResponse> orderResponses = new List<OrderResponse>();

            // لكل Seller هنعمل أوردر منفصل
            foreach (var group in groupedItems)
            {
                decimal totalPrice = 0;
                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (var cartItem in group.Items)
                {
                    var product = cartItem.Product;

                    if (product.IsDeleted || !product.IsAvailable)
                        return Result.Failure<List<OrderResponse>>(MarketErrors.NoProduct);

                    if (cartItem.Quantity > product.Quantity)
                        return Result.Failure<List<OrderResponse>>(MarketErrors.ExceedAvailableStock);

                    product.Quantity -= cartItem.Quantity;
                    totalPrice += (product.Price - product.Discount) * cartItem.Quantity;

                    orderItems.Add(new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = product.Price,
                        TotalPrice = (product.Price - product.Discount) * cartItem.Quantity
                    });
                }

                var order = new Order
                {
                    UserID = userId,
                    SellerID = group.SellerId,
                    TotalPrice = totalPrice,
                    CreatedAt = DateTime.UtcNow,
                    Status = false, 
                    OrderItems = orderItems,
                    Address = address 
                };

                await _context.Orders.AddAsync(order, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var orderResponse = new OrderResponse(
                    OrderID: order.OrderID,
                    TotalPrice: order.TotalPrice,
                    CreatedAt: order.CreatedAt,
                    Status: order.Status,
                    Address: order.Address,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemResponse(
                        ProductId: oi.ProductId,
                        ProductName: oi.Product.Name,
                        Quantity: oi.Quantity,
                        Price: oi.Price,
                        TotalPrice: oi.TotalPrice
                    )).ToList()
                );

                orderResponses.Add(orderResponse);
            }

            _context.CartItems.RemoveRange(cart.CartItems); 
            _context.Carts.Remove(cart); 
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(orderResponses);
        }

        public async Task<Result<List<OrderResponse>>> GetOrdersForUserAsync(string userId, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Where(o => o.UserID == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellationToken);

            if (orders == null || !orders.Any())
                return Result.Failure<List<OrderResponse>>(MarketErrors.OrdersNotFound);

            var orderResponses = orders.Select(order => new OrderResponse(
                OrderID: order.OrderID,
                TotalPrice: order.TotalPrice,
                CreatedAt: order.CreatedAt,
                Status: order.Status,
                Address: order.Address,
                OrderItems: order.OrderItems.Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList()
            )).ToList();

            return Result.Success(orderResponses);
        }

        public async Task<Result<List<OrderResponse>>> GetOrdersForSellerAsync(string sellerId, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.SellerID == sellerId)
                .ToListAsync(cancellationToken);

            if (!orders.Any())
                return Result.Failure<List<OrderResponse>>(MarketErrors.OrdersNotFound);

            var orderResponses = orders.Select(order => new OrderResponse(
                OrderID: order.OrderID,
                TotalPrice: order.TotalPrice,
                CreatedAt: order.CreatedAt,
                Status: order.Status,
                Address: order.Address,
                OrderItems: order.OrderItems.Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList()
            )).ToList();

            return Result.Success(orderResponses);
        }

        public async Task<Result<OrderResponse>> GetOrderByIdForUserAsync(int orderId, string userId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderID == orderId && o.UserID == userId, cancellationToken);

            if (order is null)
                return Result.Failure<OrderResponse>(MarketErrors.OrderNotFound);

            var orderResponse = new OrderResponse(
                OrderID: order.OrderID,
                TotalPrice: order.TotalPrice,
                CreatedAt: order.CreatedAt,
                Status: order.Status,
                Address: order.Address,
                OrderItems: order.OrderItems.Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList()
            );

            return Result.Success(orderResponse);
        }

        public async Task<Result<OrderResponse>> GetOrderByIdForSellerAsync(int orderId, string sellerId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderID == orderId && o.SellerID == sellerId, cancellationToken);

            if (order is null)
                return Result.Failure<OrderResponse>(MarketErrors.OrdersNotFound);

            var response = new OrderResponse(
                OrderID: order.OrderID,
                TotalPrice: order.TotalPrice,
                CreatedAt: order.CreatedAt,
                Status: order.Status,
                Address: order.Address,
                OrderItems: order.OrderItems.Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList()
            );

            return Result.Success(response);
        }


        public async Task<Result> MarkOrderAsCompletedAsync(string sellerId, int orderId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderID == orderId && o.SellerID == sellerId, cancellationToken);

            if (order is null)
                return Result.Failure(MarketErrors.OrdersNotFound);

            order.Status = true;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> CancelOrderByUserAsync(int orderId, string userId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderID == orderId && o.UserID == userId, cancellationToken);

            if (order is null)
                return Result.Failure(MarketErrors.OrderNotFound);

            if (order.Status)
                return Result.Failure(MarketErrors.OrderAlreadyCompleted);

            if (order.IsCancelled)
                return Result.Failure(MarketErrors.OrderAlreadyCancelled);

            order.IsCancelled = true;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<List<OrderResponse>>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellationToken);

            if (!orders.Any())
                return Result.Failure<List<OrderResponse>>(MarketErrors.OrdersNotFound);

            var responses = orders.Select(order => new OrderResponse(
                OrderID: order.OrderID,
                TotalPrice: order.TotalPrice,
                CreatedAt: order.CreatedAt,
                Status: order.Status,
                Address: order.Address,
                OrderItems: order.OrderItems.Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList()
            )).ToList();

            return Result.Success(responses);
        }



    }
}
