using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petsica.Core.Entities.Community;
using Petsica.Service.Abstractions.Dashboard;
using Petsica.Shared.Contracts.Dashboard.Response;
using Petsica.Shared.Contracts.Users.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Petsica.Service.Services;
public class DashboardService(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : IDashboardService
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<NumberOfUsers>> GetUserCountsByRole(CancellationToken cancellationToken = default)
    {
        
        var roleCounts = await _userManager.Users
            .GroupBy(ur => ur.Type)
            .Select(g => new { RoleName = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);
        var users = new NumberOfUsers(
            seller: roleCounts.FirstOrDefault(rc => rc.RoleName == "SELLER")?.Count ?? 0,
            user: roleCounts.FirstOrDefault(rc => rc.RoleName == "MEMBER")?.Count ?? 0,
            sitter: roleCounts.FirstOrDefault(rc => rc.RoleName == "SITTER")?.Count ?? 0,
            clinic: roleCounts.FirstOrDefault(rc => rc.RoleName == "CLINIC")?.Count ?? 0);
        return Result<NumberOfUsers>.Success(users);
        

      
    }

    public async Task<Result<List<GeneralInfoForAllUsersResponse>>> GetGeneralInfoAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userManager.Users
                          .Select(u => new GeneralInfoForAllUsersResponse(u.Id, u.UserName,u.Email,u.Type))
                          .ToListAsync();



        return Result<List<GeneralInfoForAllUsersResponse>>.Success(users);
    }

   

    public async Task<Result<EmailConfirmationStatsResponse>> GetEmailConfirmationStatsAsync(CancellationToken cancellationToken)
    {
        var users = _userManager.Users;

        var totalUsers = users.Count();
        var confirmedUsers = users.Count(u => u.EmailConfirmed);
        var unconfirmedUsers = totalUsers - confirmedUsers;

        var result = new EmailConfirmationStatsResponse
        {
            TotalUsers = totalUsers,
            ConfirmedEmails = confirmedUsers,
            UnconfirmedEmails = unconfirmedUsers
        };

        return Result.Success(result);
    }

    public async Task<List<UserActivitySummary>> GetAllTimeUserActivityAsync(CancellationToken cancellationToken = default)
    {
        // Retrieve all-time activity counts for each user
        var userActivitySummaries = await _context.Users
            .Select(user => new UserActivitySummary
            (
                user.UserID,  // Ensure UserID is the correct property in the User model
                user.Posts.Count(),
                user.Comments.Count(),
                user.Likes.Count()
            ))
            .ToListAsync(cancellationToken); // Removed the extra semicolon here

        return userActivitySummaries;
    }

    public async Task<List<UserLeaderboard>> GetTopContributorsAsync(CancellationToken cancellationToken = default)
    {
        var users = await _context.Users.ToListAsync(); // Fetch users first

        var postCounts = await _context.Posts
            .GroupBy(p => p.UserID)
            .Select(g => new { UserID = g.Key, PostsCount = g.Count() })
            .ToListAsync();

        var commentCounts = await _context.UserCommentPosts
            .GroupBy(c => c.UserID)
            .Select(g => new { UserID = g.Key, CommentsCount = g.Count() })
            .ToListAsync();

        var likeCounts = await _context.UserLikePosts
            .GroupBy(l => l.UserID)
            .Select(g => new { UserID = g.Key, LikesCount = g.Count() })
            .ToListAsync();

        var leaderboard = users.Select(u => new UserLeaderboard(
            u.UserID,
            postCounts.FirstOrDefault(p => p.UserID == u.UserID)?.PostsCount ?? 0,
            commentCounts.FirstOrDefault(c => c.UserID == u.UserID)?.CommentsCount ?? 0,
            likeCounts.FirstOrDefault(l => l.UserID == u.UserID)?.LikesCount ?? 0
        ))
        .OrderByDescending(u => u.TotalActivity)
        .Take(10)
        .ToList();

        return leaderboard;
    }

    public async Task<List<GetTopPostsResponse>> GetTopPostsAsync(CancellationToken cancellationToken=default)
    {
        var topPosts = await _context.Posts
            .OrderByDescending(p => p.LikesCount + p.CommentsCount) 
            .Take(5)
            .Select(p => new GetTopPostsResponse(
                p.PostID.ToString(),   
                p.Content,
                p.Date,
                p.Photo ?? "No Photo", 
                p.LikesCount,
                p.CommentsCount,
                p.UserID
            ))
            .ToListAsync();

        return topPosts;
    }

}



