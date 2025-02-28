using Petsica.Core.Entities.Community;
using Petsica.Service.Abstractions.Community;
using Petsica.Shared.Contracts.Community;


namespace Petsica.Service.Service.Community;
public class PostService(
    ApplicationDbContext context) : IPostService
{
    private readonly ApplicationDbContext _context = context;
    public async Task<Result<PostResponse>> AddAsync(string userId, PostRequest request, CancellationToken cancellationToken = default)
    {
        var newPost = new Post
        {
            Content = request.Content,
            UserID = userId,
        };

        await _context.Posts.AddAsync(newPost, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = newPost.Adapt<PostResponse>();

        return Result.Success(response);
    }

    public async Task<Result<List<PostResponse>>> GetAllPostsAsync(CancellationToken cancellationToken = default)
    {


        var posts = await _context.Posts
            .Include(p => p.User)
            .ToListAsync(cancellationToken);


        var response = posts.Adapt<List<PostResponse>>();

        return Result.Success(response);


    }

}
