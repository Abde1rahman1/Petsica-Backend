using Petsica.Core.Entities.Marketplace;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Contracts.Marketplace.Response;

namespace Petsica.Service.Services.Marketplace
{
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<List<OrderResponse>>> CreateOrderFromCartAsync(string userId, CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserID == userId, cancellationToken);

            if (cart == null || !cart.CartItems.Any())
                return Result.Failure<List<OrderResponse>>(MarketErrors.CartNotFound);

            var groupedItems = cart.CartItems
                .GroupBy(ci => ci.Product.SellerID)
                .Select(g => new
                {
                    SellerId = g.Key,
                    Items = g.ToList()
                })
                .ToList();

            decimal totalPrice = 0;
            List<SellerOrder> sellerOrders = new();

            foreach (var group in groupedItems)
            {
                decimal sellerTotalPrice = 0;
                List<OrderItem> sellerOrderItems = new();

                foreach (var cartItem in group.Items)
                {
                    var product = cartItem.Product;

                    if (product.IsDeleted || !product.IsAvailable)
                        return Result.Failure<List<OrderResponse>>(MarketErrors.NoProduct);

                    if (cartItem.Quantity > product.Quantity)
                        return Result.Failure<List<OrderResponse>>(MarketErrors.ExceedAvailableStock);

                    product.Quantity -= cartItem.Quantity;

                    var priceAfterDiscount = product.Price - product.Discount;
                    var totalItemPrice = priceAfterDiscount * cartItem.Quantity;

                    sellerTotalPrice += totalItemPrice;

                    var orderItem = new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = product.Price,
                        Discount = product.Discount,
                        TotalPrice = totalItemPrice
                    };

                    sellerOrderItems.Add(orderItem);
                }

                var sellerOrder = new SellerOrder
                {
                    SellerId = group.SellerId,
                    TotalPrice = sellerTotalPrice,
                    Status = false,
                    IsCancelled = false,
                    OrderItems = sellerOrderItems
                };

                totalPrice += sellerTotalPrice;
                sellerOrders.Add(sellerOrder);
            }

            var order = new Order
            {
                UserID = userId,
                TotalPrice = totalPrice,
                CreatedAt = DateTime.UtcNow,
                Status = false,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                SellerOrders = sellerOrders
            };

            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _context.CartItems.RemoveRange(cart.CartItems);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new List<OrderResponse>());
        }


        public async Task<Result<List<OrderResponse>>> GetOrdersForUserAsync(string userId, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Where(o => o.UserID == userId)
                .Include(o => o.SellerOrders)
                    .ThenInclude(so => so.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellationToken);

            if (orders == null || !orders.Any())
                return Result.Failure<List<OrderResponse>>(MarketErrors.OrdersNotFound);

            var orderResponses = orders.Select(order =>
            {
                var orderItems = order.SellerOrders
                    .SelectMany(so => so.OrderItems)
                    .Select(oi => new OrderItemResponse(
                        ProductId: oi.ProductId,
                        ProductName: oi.Product.Name,
                        Photo: oi.Product.Photo,
                        Quantity: oi.Quantity,
                        Price: oi.Price,
                        TotalPrice: oi.TotalPrice
                    )).ToList();

                var totalQuantity = orderItems.Sum(oi => oi.Quantity);
                var isCompleted = order.Status;

                return new OrderResponse(
                    OrderID: order.OrderID,
                    UserId: order.UserID,
                    TotalPrice: order.TotalPrice,
                    CreatedAt: order.CreatedAt,
                    Status: isCompleted,
                    Address: order.Address,
                    PhoneNumber: order.PhoneNumber,
                    OrderItems: orderItems,
                    TotalQuantity: totalQuantity
                );
            }).ToList();

            return Result.Success(orderResponses);
        }

        public async Task<Result<OrderResponse>> GetOrderByIdForUserAsync(int orderId, string userId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Where(o => o.OrderID == orderId && o.UserID == userId)
                .Include(o => o.SellerOrders)
                    .ThenInclude(so => so.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
                return Result.Failure<OrderResponse>(MarketErrors.OrderNotFound);

            var orderItems = order.SellerOrders
                .SelectMany(so => so.OrderItems)
                .Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Photo: oi.Product.Photo,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList();

            var totalQuantity = orderItems.Sum(oi => oi.Quantity);

            var orderResponse = new OrderResponse(
                OrderID: order.OrderID,
                UserId: order.UserID,
                TotalPrice: order.TotalPrice,
                CreatedAt: order.CreatedAt,
                Status: order.Status,
                Address: order.Address,
                PhoneNumber: order.PhoneNumber,
                OrderItems: orderItems,
                TotalQuantity: totalQuantity
            );

            return Result.Success(orderResponse);
        }


        public async Task<Result<List<SellerOrderResponse>>> GetOrdersForSellerAsync(string sellerId, CancellationToken cancellationToken)
        {
            var sellerOrders = await _context.SellerOrder
                .Where(so => so.SellerId == sellerId)
                .Include(so => so.Order)
                .Include(so => so.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellationToken);

            if (!sellerOrders.Any())
                return Result.Failure<List<SellerOrderResponse>>(MarketErrors.OrdersNotFound);

            var sellerOrderResponses = sellerOrders.Select(so => new SellerOrderResponse(
                SellerOrderId: so.SellerOrderId,
                OrderId: so.OrderId,
                SellerId: so.SellerId,
                CreatedAt: so.Order.CreatedAt,
                TotalQuantity: so.OrderItems.Sum(oi => oi.Quantity),
                TotalPrice: so.TotalPrice,
                Status: so.Status,
                IsCancelled: so.IsCancelled,
                OrderItems: so.OrderItems.Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Photo: oi.Product.Photo,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList()
            )).ToList();

            return Result.Success(sellerOrderResponses);
        }


        public async Task<Result<SellerOrderResponse>> GetSellerOrderByIdAsync(int sellerOrderId, string sellerId, CancellationToken cancellationToken)
        {
            var sellerOrder = await _context.SellerOrder
                .Include(so => so.Order)
                .Include(so => so.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(so => so.SellerOrderId == sellerOrderId && so.SellerId == sellerId, cancellationToken);

            if (sellerOrder is null)
                return Result.Failure<SellerOrderResponse>(MarketErrors.OrdersNotFound);

            var response = new SellerOrderResponse(
                SellerOrderId: sellerOrder.SellerOrderId,
                OrderId: sellerOrder.OrderId,
                SellerId: sellerOrder.SellerId,
                TotalPrice: sellerOrder.TotalPrice,
                Status: sellerOrder.Status,
                IsCancelled: sellerOrder.IsCancelled,
                CreatedAt: sellerOrder.Order.CreatedAt,
                TotalQuantity: sellerOrder.OrderItems.Sum(oi => oi.Quantity),
                OrderItems: sellerOrder.OrderItems.Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Photo: oi.Product.Photo,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList()
            );

            return Result.Success(response);
        }




        public async Task<Result> MarkSellerOrderAsCompletedAsync(string sellerId, int sellerOrderId, CancellationToken cancellationToken)
        {
            var sellerOrder = await _context.SellerOrder
                .FirstOrDefaultAsync(so => so.SellerOrderId == sellerOrderId && so.SellerId == sellerId, cancellationToken);

            if (sellerOrder is null)
                return Result.Failure(MarketErrors.OrdersNotFound);

            if (sellerOrder.IsCancelled)
                return Result.Failure(MarketErrors.SellerOrderIsCancelled);

            if (sellerOrder.Status)
                return Result.Failure(MarketErrors.SellerOrderAlreadyCompleted);

            sellerOrder.Status = true;

            _context.SellerOrder.Update(sellerOrder);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> MarkOrderAsCompletedByAdminAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.SellerOrders)
                .FirstOrDefaultAsync(o => o.OrderID == orderId, cancellationToken);

            if (order is null)
                return Result.Failure(MarketErrors.OrderNotFound);

            if (order.SellerOrders.Any(so => !so.Status))
                return Result.Failure(MarketErrors.NotAllSellerOrdersCompleted);


            order.Status = true;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        public async Task<Result> CancelOrderByUserAsync(int orderId, string userId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.SellerOrders)
                .FirstOrDefaultAsync(o => o.OrderID == orderId && o.UserID == userId, cancellationToken);

            if (order is null)
                return Result.Failure(MarketErrors.OrderNotFound);

            if (order.IsCancelled)
                return Result.Failure(MarketErrors.OrderAlreadyCancelled);

            if (order.Status)
                return Result.Failure(MarketErrors.OrderAlreadyCompleted);

            if (order.SellerOrders.Any(so => so.Status))
                return Result.Failure(MarketErrors.OrderPartiallyCompleted);

            order.IsCancelled = true;

            foreach (var sellerOrder in order.SellerOrders)
            {
                sellerOrder.IsCancelled = true;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> CancelOrderByAdminAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.SellerOrders)
                .FirstOrDefaultAsync(o => o.OrderID == orderId, cancellationToken);

            if (order is null)
                return Result.Failure(MarketErrors.OrderNotFound);

            if (order.IsCancelled)
                return Result.Failure(MarketErrors.OrderAlreadyCancelled);

            if (order.Status)
                return Result.Failure(MarketErrors.OrderAlreadyCompleted);

            order.IsCancelled = true;

            foreach (var sellerOrder in order.SellerOrders)
            {
                sellerOrder.IsCancelled = true;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }


        public async Task<Result<List<AdminOrderResponse>>> GetAllOrdersForAdminAsync(CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Include(o => o.SellerOrders)
                    .ThenInclude(so => so.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellationToken);

            if (!orders.Any())
                return Result.Failure<List<AdminOrderResponse>>(MarketErrors.OrdersNotFound);

            var responses = orders.Select(order =>
            {
                var orderItems = order.SellerOrders
                    .SelectMany(so => so.OrderItems)
                    .Select(oi => new OrderItemResponse(
                        ProductId: oi.ProductId,
                        ProductName: oi.Product.Name,
                        Photo: oi.Product.Photo,
                        Quantity: oi.Quantity,
                        Price: oi.Price,
                        TotalPrice: oi.TotalPrice
                    )).ToList();

                int totalQuantity = orderItems.Sum(oi => oi.Quantity);

                return new AdminOrderResponse(
                    OrderID: order.OrderID,
                    UserId: order.UserID,
                    TotalQuantity: totalQuantity,
                    TotalPrice: order.TotalPrice,
                    CreatedAt: order.CreatedAt,
                    Status: order.Status,
                    Address: order.Address,
                    PhoneNumber: order.PhoneNumber,
                    OrderItems: orderItems
                );
            }).ToList();

            return Result.Success(responses);
        }



        public async Task<Result<List<AdminSellerOrderResponse>>> GetAllSellerOrdersForAdminAsync(CancellationToken cancellationToken)
        {
            var sellerOrders = await _context.SellerOrder
                .Include(so => so.Order)
                .Include(so => so.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellationToken);

            if (!sellerOrders.Any())
                return Result.Failure<List<AdminSellerOrderResponse>>(MarketErrors.SellerOrdersNotFound);

            var responses = sellerOrders.Select(so =>
            {
                var orderItems = so.OrderItems.Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Photo: oi.Product.Photo,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList();

                int totalQuantity = orderItems.Sum(oi => oi.Quantity);

                return new AdminSellerOrderResponse(
                    SellerOrderId: so.SellerOrderId,
                    OrderId: so.OrderId,
                    UserId: so.Order.UserID,
                    SellerId: so.SellerId,
                    CreatedAt: so.Order.CreatedAt,
                    Status: so.Status,
                    IsCancelled: so.IsCancelled,
                    TotalQuantity: totalQuantity,
                    TotalPrice: so.TotalPrice,
                    OrderItems: orderItems
                );

            }).ToList();

            return Result.Success(responses);
        }


        public async Task<Result<AdminSellerOrderResponse>> GetSellerOrderByIdForAdminAsync(int sellerOrderId, CancellationToken cancellationToken)
        {
            var so = await _context.SellerOrder
                .Include(so => so.Order)
                .Include(so => so.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(so => so.SellerOrderId == sellerOrderId, cancellationToken);

            if (so == null)
                return Result.Failure<AdminSellerOrderResponse>(MarketErrors.SellerOrdersNotFound);

            var orderItems = so.OrderItems.Select(oi => new OrderItemResponse(
                ProductId: oi.ProductId,
                ProductName: oi.Product.Name,
                Photo: oi.Product.Photo,
                Quantity: oi.Quantity,
                Price: oi.Price,
                TotalPrice: oi.TotalPrice
            )).ToList();

            var totalQuantity = orderItems.Sum(oi => oi.Quantity);

            var response = new AdminSellerOrderResponse(
                SellerOrderId: so.SellerOrderId,
                OrderId: so.OrderId,
                UserId: so.Order.UserID,
                SellerId: so.SellerId,
                CreatedAt: so.Order.CreatedAt,
                Status: so.Status,
                IsCancelled: so.IsCancelled,
                TotalQuantity: totalQuantity,
                TotalPrice: so.TotalPrice,
                OrderItems: orderItems
            );

            return Result.Success(response);
        }



        public async Task<Result<AdminOrderResponse>> GetOrderByIdForAdminAsync(int orderId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.SellerOrders)
                    .ThenInclude(so => so.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderID == orderId, cancellationToken);

            if (order == null)
                return Result.Failure<AdminOrderResponse>(MarketErrors.OrderNotFound);

            var orderItems = order.SellerOrders
                .SelectMany(so => so.OrderItems)
                .Select(oi => new OrderItemResponse(
                    ProductId: oi.ProductId,
                    ProductName: oi.Product.Name,
                    Photo: oi.Product.Photo,
                    Quantity: oi.Quantity,
                    Price: oi.Price,
                    TotalPrice: oi.TotalPrice
                )).ToList();

            var totalQuantity = orderItems.Sum(oi => oi.Quantity);

            var response = new AdminOrderResponse(
                OrderID: order.OrderID,
                UserId: order.UserID,
                TotalQuantity: totalQuantity,
                TotalPrice: order.TotalPrice,
                CreatedAt: order.CreatedAt,
                Status: order.Status,
                Address: order.Address,
                PhoneNumber: order.PhoneNumber,
                OrderItems: orderItems
            );

            return Result.Success(response);
        }


    }
}
