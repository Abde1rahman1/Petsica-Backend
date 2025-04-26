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
    Task<Result<NumberOfConfirmidEmail>> GetNumberOfConfirmidEmailAsync(CancellationToken cancellationToken = default);
}
