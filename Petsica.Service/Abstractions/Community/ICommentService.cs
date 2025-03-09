using Microsoft.AspNetCore.Mvc;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Contracts.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Community;
public interface ICommentService
{
	Task<Result<CommentResponse>> AddAsync(string userId, int PostID, CommentRequest request, CancellationToken cancellationToken = default);
	Task<Result<List<CommentResponse>>> GetCommentsByPostId(int PostId, CancellationToken cancellationToken = default);

	Task<Result<CommentResponse>> UpdatePostById(int CommentId, [FromBody] CommentRequest request, CancellationToken cancellationToken = default);

	Task<Result<CommentResponse>> DeleteById(int CommentId, CancellationToken cancellationToken);
}
