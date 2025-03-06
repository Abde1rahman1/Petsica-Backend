using Microsoft.AspNetCore.Mvc;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Contracts.Community.Response;
using Petsica.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Community;
public interface IPostService
{
    Task<Result<PostResponse>> AddAsync(string userId, PostRequest request, CancellationToken cancellationToken = default);
	Task<Result<List<PostResponse>>> GetAllPostsAsync(CancellationToken cancellationToken = default);

	Task<Result<PostResponse>> UpdatePostById(int PostId, [FromBody] PostRequest request, CancellationToken cancellationToken = default);

	Task<Result<PostResponse>> DeleteById(int PostId, CancellationToken cancellationToken);

	Task<Result<List<PostResponse>>> GetPostById(int postId, CancellationToken cancellationToken = default);
}
