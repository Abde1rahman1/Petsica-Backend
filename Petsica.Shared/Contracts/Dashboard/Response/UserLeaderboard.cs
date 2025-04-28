using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Dashboard.Response;
public record UserLeaderboard
(
    string UserID,

    int PostsCount,
    int CommentsCount,
    int LikesCount
)
{
    public int TotalActivity => PostsCount + CommentsCount + LikesCount; // Computed property
}
