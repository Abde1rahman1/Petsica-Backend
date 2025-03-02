
using System.Text.Json.Serialization;

namespace Petsica.Core.Entities.Community;
public class Post
{
    public int PostID { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Photo { get; set; }

    public bool IsDeleted { get; set; } = false; 

	#region Foreign Key
	public string UserID { get; set; }
    #endregion



    #region Navigation Property

    public User User { get; set; }

	public List<UserCommentPost> Comments { get; set; } = new List<UserCommentPost>();
	public List<UserLikePost> Likes { get; set; } = new List<UserLikePost>();
	#endregion
}
