

namespace Petsica.Core.Entities.Community;
public class UserCommentPost
{
    public int CommentID { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }

    #region Foreign Keys
    public int PostID { get; set; }
    public string UserID { get; set; }
    #endregion

    #region Navigation Properties
    public Post Post { get; set; }
    public User User { get; set; }
    #endregion
}
