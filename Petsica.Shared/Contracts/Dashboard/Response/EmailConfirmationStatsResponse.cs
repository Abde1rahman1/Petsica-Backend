using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Dashboard.Response;
public class EmailConfirmationStatsResponse
{
    public int TotalUsers { get; set; }
    public int ConfirmedEmails { get; set; }
    public int UnconfirmedEmails { get; set; }
}
