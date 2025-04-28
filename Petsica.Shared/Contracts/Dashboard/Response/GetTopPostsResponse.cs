using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Dashboard.Response;
public record GetTopPostsResponse
(
    string PostID,
    string Content,
    DateTime Date,
    string Photo,
    int LikesCount,
    int CommentsCount,
    string UserID
);

