

namespace Petsica.Core.Entities.Users;
public class User
{
    public int UserID { get; set; }
    public string Photo { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public string Location { get; set; }

    public string? NationalID { get; set; }
    public string Type { get; set; }

    #region Navigation Properities
    public List<Pet> Pets { get; set; }
    public List<Post> Posts { get; set; }
    public List<UserCommentPost> Comments { get; set; }
    public List<UserLikePost> Likes { get; set; }
    public List<Order> Orders { get; set; }
    public List<UserRequestService> RequestedServices { get; set; }
    #endregion
}