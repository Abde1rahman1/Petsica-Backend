using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Error
{
    public static class MarketErrors
    {
		public static readonly Errors InvalidCreateProduct =
			new("Product.InvalidCreateProduct", "Failed to create the Product", StatusCodes.Status400BadRequest);

		public static readonly Errors InvalidCategory =
			new("Product.InvalidCategory", "Invalid Category, Choose Correct Category", StatusCodes.Status400BadRequest);

		public static readonly Errors GetAllProductsFailed =
			new("Product.GetAllProductsFailed", "Faild While Get Products", StatusCodes.Status400BadRequest);

		public static readonly Errors NoProductBySeller =
		new("Product.NoProductBySeller", "No Product belongs to you !", StatusCodes.Status404NotFound);

		public static readonly Errors NoProduct =
		new("Product.NoProduct", "No Product ", StatusCodes.Status404NotFound);

		public static readonly Errors Unathorized =
		new("Product.Unathorized", "Unauthorized to update ! ", StatusCodes.Status401Unauthorized);

        public static readonly Errors ExceedAvailableStock = 
			new("Cart.ExceedAvailableStock", "Requested quantity exceeds available stock.", StatusCodes.Status400BadRequest);

        public static readonly Errors CartNotFound =
        new("Cart.NotFound", "Cart not found.", StatusCodes.Status404NotFound);

        public static readonly Errors CartItemNotFound =
            new("Cart.ItemNotFound", "This item does not exist in the cart.", StatusCodes.Status404NotFound);

        public static readonly Errors OrdersNotFound =
        new("Orders.NotFound", "No orders were found for the given user.", StatusCodes.Status404NotFound);

        public static readonly Errors OrderNotFound =
		new("Order.NotFound", "This order does not exist.", StatusCodes.Status404NotFound);

        public static readonly Errors OrderAlreadyCompleted =
		new("Order.AlreadyCompleted", "This order has already been completed and cannot be cancelled.", StatusCodes.Status400BadRequest);

        public static readonly Errors OrderAlreadyCancelled =
		new("Order.AlreadyCancelled", "This order has already been cancelled.", StatusCodes.Status400BadRequest);


    }
}
