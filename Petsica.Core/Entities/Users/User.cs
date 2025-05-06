

namespace Petsica.Core.Entities.Users;
public class User
{
    public string UserID { get; set; }

    #region Navigation Properities
    public List<Pet>? Pets { get; set; }
    public List<Post>? Posts { get; set; }
    public List<UserCommentPost>? Comments { get; set; }
    public List<UserLikePost>? Likes { get; set; }
    public List<Order>? Orders { get; set; }
    public List<UserRequestService>? RequestedServices { get; set; }
    public List<UserFollow>? UserFollowers { get; set; }

    #endregion
}
