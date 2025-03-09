using Petsica.Shared.Contracts.Community;

namespace Petsica.Service.Abstractions.Community;
public interface IPostService
{
    Task<Result<PostResponse>> AddAsync(string userId, PostRequest request, CancellationToken cancellationToken = default);
    Task<Result<List<PostResponse>>> GetAllPostsAsync(CancellationToken cancellationToken = default);
}
