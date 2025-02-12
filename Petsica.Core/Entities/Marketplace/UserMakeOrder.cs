
namespace Petsica.Core.Entities.Marketplace;
public class UserMakeOrder
{
    public string UserID { get; set; }
    public int OrderID { get; set; }

    #region Navigation Properties
    public User User { get; set; }
    public Order Order { get; set; }
    #endregion
}
