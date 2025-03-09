
namespace Petsica.Core.Entities.Services;
public class UserRequestService
{
    public int ServiceID { get; set; }
    public string UserID { get; set; }


    #region Navigation Properties
    public SitterService? Service { get; set; }
    public User? User { get; set; }
    #endregion
}
