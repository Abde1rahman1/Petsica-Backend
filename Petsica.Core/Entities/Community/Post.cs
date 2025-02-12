
namespace Petsica.Core.Entities.Community;
public class Post
{
    public int PostID { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public string Photo { get; set; }

    #region Foreign Key
    public string UserID { get; set; }
    #endregion

    #region Navigation Property
    public User User { get; set; }
    public List<UserCommentPost> Comments { get; set; }
    public List<UserLikePost> Likes { get; set; }
    #endregion
}
