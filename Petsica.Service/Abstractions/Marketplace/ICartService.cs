using Org.BouncyCastle.Asn1.Cmp;
using Petsica.Core.Entities.Marketplace;
using Petsica.Shared.Contracts.Marketplace.Request;
using Petsica.Shared.Contracts.Marketplace.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Marketplace
{
    public interface ICartService
    {
        Task<Result> AddToCartAsync(string userId, AddToCartRequest request, CancellationToken cancellationToken = default);
        Task<Result<CartResponse>> GetCartItemsAsync(string userId, CancellationToken cancellationToken = default);
        Task<Result> RemoveFromCartAsync(string userId, int productId, CancellationToken cancellationToken = default);
        Task<Result> UpdateCartItemAsync(string userId, UpdateCartItemRequest request, CancellationToken cancellationToken = default);
        Task<Result> ClearCartAsync(string userId, CancellationToken cancellationToken = default);
    }
}
