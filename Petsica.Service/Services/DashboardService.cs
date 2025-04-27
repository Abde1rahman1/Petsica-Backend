using Microsoft.AspNetCore.Identity;
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

    public async Task<Result<NumberOfConfirmidEmail>> GetNumberOfConfirmidEmailAsync(CancellationToken cancellationToken = default)
    {
        
        var confirmedCount = await _userManager.Users
            .CountAsync(u => u.EmailConfirmed, cancellationToken);

        var result = new NumberOfConfirmidEmail(
            TotalNumber: confirmedCount
        );

        return Result<NumberOfConfirmidEmail>.Success(result);
    }

    //public async Task<Result<NumberOfConfirmidEmail>> GetNumberOfClinicsAsync(CancellationToken cancellationToken = default)
    //{

    //    var Count = await _userManager.Users
    //        .CountAsync(u => u.Type, cancellationToken);

    //    var result = new NumberOfConfirmidEmail(
    //        TotalNumber: Count
    //    );

    //    return Result<NumberOfConfirmidEmail>.Success(result);
    //}
}
