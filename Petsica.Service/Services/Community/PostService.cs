﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Petsica.Core.Entities.Community;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community.Request;
using Petsica.Shared.Contracts.Community.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Petsica.Shared.Const;


namespace Petsica.Service.Services.Community;
public class PostService(
    ApplicationDbContext context) : IPostService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<Result<PostResponse>> AddAsync(string userId, PostRequest request, CancellationToken cancellationToken = default)
    {

        var newPost = new Post
        {
            Content = request.Content,
			Photo= request.photo,
            UserID = userId,
        };

        await _context.Posts.AddAsync(newPost, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = newPost.Adapt<PostResponse>();

        return Result.Success(response);
    }

    public async Task<Result> UpdatePostById(string userId ,int PostId, [FromBody] PostRequest request, CancellationToken cancellationToken = default)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostID == PostId, cancellationToken);

		if (post is null)
			return Result.Failure(CommunityErrors.InvalidPostId);

		if (post.UserID != userId)
			return Result.Failure(CommunityErrors.UnauthorizedToUpdatePost);

		post.Content = request.Content;

        _context.Posts.Update(post);
        _context.SaveChanges();

        var response = post.Adapt<PostResponse>();
        return Result.Success(response);
    }
    public async Task<Result> DeleteById(string userId, int PostId, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostID == PostId);
		if (post is null)
			return Result.Failure(CommunityErrors.InvalidPostId);

		if (post.UserID != userId)
			return Result.Failure(CommunityErrors.UnauthorizedToUpdatePost);
		post.IsDeleted = true;
		_context.Posts.Update(post);
		_context.SaveChanges();

		var response = post.Adapt<PostResponse>();
		return Result.Success(response);
	}


    public async Task<Result<List<PostResponse>>> GetAllPostsAsync(CancellationToken cancellationToken = default)
    {


        var posts = await _context.Posts
            .Include(p => p.User)
			.Include(p => p.Likes)
			.Include(p => p.Comments)
			.Where(p=>p.IsDeleted==false)
			.OrderBy(p=>p.Date)
            .ToListAsync(cancellationToken);



		var response = posts.Select(post => new PostResponse
		(
			Id: post.PostID,
			Content: post.Content,
			userId: post.UserID,
			Date: post.Date,
			Photo: post.Photo,
			LikesCount: post.Likes.Count,
			CommentsCount: post.Comments.Count
		)).ToList();
		return Result.Success(response); 

	}
	public async Task<Result<List<PostResponse>>> GetPostById(int postId,CancellationToken cancellationToken = default)
	{


		var posts = await _context.Posts
			.Include(p => p.User)
			.Include(p => p.Likes) 
			.Include(p => p.Comments) 
			.Where(p => p.IsDeleted == false && p.PostID == postId)
			.ToListAsync(cancellationToken);

	
		
		var response = posts.Select(post => new PostResponse
		(
			Id: post.PostID,
			Content: post.Content,
			userId: post.UserID,
			Date: post.Date,
			Photo: post.Photo,
			LikesCount: post.Likes.Count, 
			CommentsCount: post.Comments.Count 
		)).ToList();

		return Result.Success(response);
	}

    public async Task<Result<List<PostResponse>>> GetMyPostsAsync(string userId,CancellationToken cancellationToken = default)
    {


        var posts = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .Where(p => p.IsDeleted == false && p.UserID == userId)
            .OrderBy(p => p.Date)
            .ToListAsync(cancellationToken);



        var response = posts.Select(post => new PostResponse
        (
            Id: post.PostID,
            Content: post.Content,
            userId: post.UserID,
            Date: post.Date,
            Photo: post.Photo,
            LikesCount: post.Likes.Count,
            CommentsCount: post.Comments.Count
        )).ToList();
        return Result.Success(response);

    }

}
