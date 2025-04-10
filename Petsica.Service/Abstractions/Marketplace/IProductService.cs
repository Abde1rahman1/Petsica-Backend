using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Contracts.Marketplace.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Marketplace
{
    public interface IProductService
    {
		Task<Result> AddAsync(string SellerId, ProductRequest request, CancellationToken cancellationToken = default);

		Task<Result<List<ProductResponse>>> GetAllProductsAsync(CancellationToken cancellationToken = default);

		Task<Result<List<ProductResponse>>> GetMyProductsAsync(string sellerId, CancellationToken cancellationToken = default);

		Task<Result<List<ProductResponse>>> GetProductsByCategoryAsync(string category, CancellationToken cancellationToken = default);

		Task<Result<ProductResponse>> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default);

		Task<Result> UpdateProductById(string sellerId, int productId, UpdateProductRequest request, CancellationToken cancellationToken = default);

		Task<Result> ProductSoldoutAsync(string sellerId, int productId, CancellationToken cancellationToken);

		Task<Result> DeleteById(string sellerId, int productId, CancellationToken cancellationToken);

	}
}
