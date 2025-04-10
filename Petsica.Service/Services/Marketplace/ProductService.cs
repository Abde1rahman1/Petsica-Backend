using Microsoft.AspNetCore.Mvc;
using Petsica.Core.Entities.Community;
using Petsica.Core.Entities.Marketplace;
using Petsica.Service.Abstractions.Marketplace;
using Petsica.Shared.Const;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Contracts.Community.Response;
using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Contracts.Marketplace.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Petsica.Core.Const.DefaultRoles;

namespace Petsica.Service.Services.Marketplace
{
    public class ProductService (ApplicationDbContext context) : IProductService
    {
		private readonly ApplicationDbContext _context = context;

		public async Task<Result> AddAsync(string SellerId, ProductRequest request, CancellationToken cancellationToken = default)
		{
			if (!Enum.TryParse<ProductCategory>(request.Category, true, out ProductCategory category))
				return Result.Failure(MarketErrors.InvalidCategory);

			var newProduct = new Product
			{
				Name = request.ProductName,
				SellerID = SellerId,
				Description = request.Description,
				Discount = request.Discount,
				Photo = request.Photo,
				Quantity = request.Quantity,
				Price = request.Price,
				Category = category,


			};
			try
			{
				await _context.Products.AddAsync(newProduct, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				
				return Result.Failure(MarketErrors.InvalidCreateProduct);
			}


			return Result.Success();
		}

		public async Task<Result<List<ProductResponse>>> GetAllProductsAsync(CancellationToken cancellationToken = default)
		{

			try
			{
				var products = await _context.Products
								.Where(p => p.IsDeleted == false)
								.ToListAsync(cancellationToken);

				var response = products.Select(product => new ProductResponse
				(
					productId: product.ProductID,
					Price: product.Price,
					Discount: product.Discount,
					Description: product.Description,
					Quantity: product.Quantity,
					ProductName: product.Name,
					Photo: product.Photo,
					Category: product.Category,
					SellerId: product.SellerID,
					IsAvailable: product.IsAvailable
				)).ToList();
				return Result.Success(response);
			}
			catch (Exception ex) 
			{
				return Result.Failure<List<ProductResponse>>(MarketErrors.GetAllProductsFailed);
			}

		}

		public async Task<Result<List<ProductResponse>>> GetMyProductsAsync(string sellerId,CancellationToken cancellationToken = default)
		{

			try
			{
				var products = await _context.Products
								.Where(p => p.IsDeleted == false && p.SellerID == sellerId)
								.ToListAsync(cancellationToken);
				if (products == null)
					return Result.Failure<List<ProductResponse>>(MarketErrors.NoProductBySeller);

				var response = products.Select(product => new ProductResponse
				(
					productId: product.ProductID,
					Price: product.Price,
					Discount: product.Discount,
					Description: product.Description,
					Quantity: product.Quantity,
					ProductName: product.Name,
					Photo: product.Photo,
					Category: product.Category,
					SellerId: product.SellerID,
					IsAvailable: product.IsAvailable
				)).ToList();
				return Result.Success(response);
			}
			catch (Exception ex)
			{
				return Result.Failure<List<ProductResponse>>(MarketErrors.GetAllProductsFailed);
			}

		}

		public async Task<Result<List<ProductResponse>>> GetProductsByCategoryAsync(string category, CancellationToken cancellationToken = default)
		{

			try
			{
				if (!Enum.TryParse<ProductCategory>(category, true, out var categoryEnum))
					return Result.Failure<List<ProductResponse>>(MarketErrors.InvalidCategory);

				
				var products = await _context.Products
					.Where(p => !p.IsDeleted && p.Category == categoryEnum)
					.ToListAsync(cancellationToken);
				if (products == null)
					return Result.Failure<List<ProductResponse>>(MarketErrors.InvalidCategory);

				var response = products.Select(product => new ProductResponse
				(
					productId: product.ProductID,
					Price: product.Price,
					Discount: product.Discount,
					Description: product.Description,
					Quantity: product.Quantity,
					ProductName: product.Name,
					Photo: product.Photo,
					Category: product.Category,
					SellerId: product.SellerID,
					IsAvailable: product.IsAvailable
				)).ToList();
				return Result.Success(response);
			}
			catch (Exception ex)
			{
				return Result.Failure<List<ProductResponse>>(MarketErrors.GetAllProductsFailed);
			}

		}

		public async Task<Result<ProductResponse>> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default)
		{

			try
			{

				var product = await _context.Products
									.FirstOrDefaultAsync(p => !p.IsDeleted && p.ProductID == productId,cancellationToken);

				if (product == null)
					return Result.Failure<ProductResponse>(MarketErrors.NoProduct);

				var response =  new ProductResponse
				(
					productId: product.ProductID,
					Price: product.Price,
					Discount: product.Discount,
					Description: product.Description,
					Quantity: product.Quantity,
					ProductName: product.Name,
					Photo: product.Photo,
					Category: product.Category,
					SellerId: product.SellerID,
					IsAvailable:product.IsAvailable
				);
				return Result.Success(response);
			}
			catch (Exception ex)
			{
				return Result.Failure<ProductResponse>(MarketErrors.GetAllProductsFailed);
			}

		}

		public async Task<Result> UpdateProductById(string sellerId, int productId, UpdateProductRequest request, CancellationToken cancellationToken = default)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId && !p.IsDeleted, cancellationToken);

			if (product.SellerID !=  sellerId)
				return Result.Failure(MarketErrors.Unathorized);


			if (!Enum.TryParse<ProductCategory>(request.Category, true, out ProductCategory category))
				return Result.Failure(MarketErrors.InvalidCategory);

			product.Description = request.Description;
			product.Quantity = request.Quantity;
			product.Price = request.Price;
			product.Discount = request.Discount;
			product.Description = request.Description;
			product.Quantity = request.Quantity;
			product.Photo = request.Photo;
			product.Name =request.ProductName;
			product.IsAvailable= request.IsAvailable;

			_context.Products.Update(product);
			_context.SaveChanges();
			return Result.Success();
		}

		public async Task<Result> DeleteById(string sellerId, int productId, CancellationToken cancellationToken)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId && !p.IsDeleted, cancellationToken);

			if (product==null)
				return Result.Failure(MarketErrors.NoProduct);

			if (product.SellerID != sellerId)
				return Result.Failure(MarketErrors.Unathorized);

			product.IsDeleted = true;
			_context.Products.Update(product);
			_context.SaveChanges();


			return Result.Success();
		}

		public async Task<Result> ProductSoldoutAsync(string sellerId, int productId, CancellationToken cancellationToken)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId && !p.IsDeleted, cancellationToken);

			if (product == null)
				return Result.Failure(MarketErrors.NoProduct);

			if (product.SellerID != sellerId)
				return Result.Failure(MarketErrors.Unathorized);

			product.IsAvailable = false;
			_context.Products.Update(product);
			_context.SaveChanges();


			return Result.Success();
		}
	}
}
