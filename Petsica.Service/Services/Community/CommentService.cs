using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Petsica.Core.Entities.Community;
using Petsica.Core.Entities.Users;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Contracts.Community.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Service.Community;
public class CommentService(ApplicationDbContext context) : ICommentService
{
	private readonly ApplicationDbContext _context = context;

	public async Task<Result<CommentResponse>> AddAsync(string userId, int PostID, CommentRequest request, CancellationToken cancellationToken = default)
	{
		var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostID == PostID, cancellationToken);
		var newComment = new UserCommentPost
		{
			Content= request.Content,
			PostID = PostID,
			UserID= userId
		};
		_context.UserCommentPosts.Add(newComment);
		await _context.SaveChangesAsync();

		var response = new CommentResponse
		(
			CommentId: newComment.CommentID,
			UserID: newComment.UserID,
			Content: newComment.Content,
			Date:newComment.Date
		);

		return Result.Success(response);

	}

	public async Task<Result<List<CommentResponse>>> GetCommentsByPostId(int PostId, CancellationToken cancellationToken = default)
	{
		var post = await _context.UserCommentPosts.FirstOrDefaultAsync(p => p.PostID == PostId,cancellationToken);
		var comments = await _context.UserCommentPosts
			.Where(p => p.PostID == PostId && p.IsDeleted==false)
			.Select(c => new UserCommentPost { Content = c.Content, UserID = c.UserID, CommentID= c.CommentID,Date= c.Date })
			.ToListAsync(cancellationToken);

		var response = new List<CommentResponse>();

		foreach (var comment in comments)
		{
			var commentResponse = new CommentResponse
			(
				CommentId:comment.CommentID,
				UserID: comment.UserID,
				Content:comment.Content,
				Date:comment.Date
			);

			response.Add(commentResponse);
		}

		return Result.Success(response);
	}

	public async Task<Result<CommentResponse>> UpdatePostById(int CommentId, [FromBody] CommentRequest request, CancellationToken cancellationToken = default)
	{
		var comment = await _context.UserCommentPosts.FirstOrDefaultAsync(p => p.CommentID == CommentId, cancellationToken);

		comment.Content = request.Content;

		_context.UserCommentPosts.Update(comment);
		_context.SaveChanges();

		var response = new CommentResponse
		(
			CommentId: comment.CommentID,
			UserID: comment.UserID,
			Content: comment.Content,
            Date: comment.Date
        );
		return Result.Success(response);
	}

	public async Task<Result<CommentResponse>> DeleteById(int CommentId, CancellationToken cancellationToken)
	{
		var comment = await _context.UserCommentPosts.FirstOrDefaultAsync(p => p.CommentID == CommentId);

		comment.IsDeleted = true;
		_context.UserCommentPosts.Update(comment);
		_context.SaveChanges();
		var response = new CommentResponse
		(
			CommentId: comment.CommentID,
			UserID: comment.UserID,
			Content: comment.Content,
            Date: comment.Date
        );
		return Result.Success(response);
	}

}
