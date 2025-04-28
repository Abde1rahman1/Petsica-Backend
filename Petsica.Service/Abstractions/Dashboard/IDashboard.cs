using Petsica.Core.Entities.Community;
using Petsica.Shared.Contracts.Dashboard.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Service.Abstractions.Dashboard;
public interface IDashboardService
{
    Task<Result<NumberOfUsers>> GetUserCountsByRole(CancellationToken cancellationToken = default);
    Task<Result<List<GeneralInfoForAllUsersResponse>>> GetGeneralInfoAsync(CancellationToken cancellationToken = default);
    Task<Result<EmailConfirmationStatsResponse>> GetEmailConfirmationStatsAsync(CancellationToken cancellationToken);

    Task<List<UserActivitySummary>> GetAllTimeUserActivityAsync(CancellationToken cancellationToken = default);
    Task<List<UserLeaderboard>> GetTopContributorsAsync(CancellationToken cancellationToken = default);
    Task<List<GetTopPostsResponse>> GetTopPostsAsync(CancellationToken cancellationToken = default);
}
