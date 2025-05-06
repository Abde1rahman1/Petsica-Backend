using Petsica.Shared.Contracts.Dashboard.Response;

namespace Petsica.Service.Abstractions.Dashboard;
public interface IDashboardService
{
    Task<Result<NumberOfUsers>> GetUserCountsByRole(CancellationToken cancellationToken = default);
    Task<Result<List<GeneralInfoForAllUsersResponse>>> GetGeneralInfoAsync(CancellationToken cancellationToken = default);
    Task<Result<EmailConfirmationStatsResponse>> GetEmailConfirmationStatsAsync(CancellationToken cancellationToken);

    Task<List<UserActivitySummary>> GetAllTimeUserActivityAsync(CancellationToken cancellationToken = default);
    Task<List<UserLeaderboard>> GetTopContributorsAsync(CancellationToken cancellationToken = default);
    Task<List<GetTopPostsResponse>> GetTopPostsAsync(CancellationToken cancellationToken = default);
    Task<int> GetTotalOrdersAsync();
    Task<decimal> GetTotalRevenueAsync();
    Task<int> GetActiveProductsAsync();
    Task<int> GetCancelledOrdersAsync();
    Task<int> GetTotalSellersAsync();

    Task<List<TopSellingProduct>> GetTopSellingProductsAsync();

    Task<List<TopSellingSeller>> GetTopSellingSellersAsync();

    Task<List<CategoryWithProductCount>> GetCategoriesWithMostProductsAsync();

}
