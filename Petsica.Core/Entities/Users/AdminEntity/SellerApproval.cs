

namespace Petsica.Core.Entities.Users.AdminEntity;
public class SellerApproval
{
    public string ApprovalID { get; set; }
    public string Status { get; set; }
    public DateTime ApprovalDate { get; set; }

    #region Foreign Keys
    public string AdminID { get; set; }
    public string SellerID { get; set; }
    #endregion
    #region Navigation Properties
    public Admin Admin { get; set; }
    public User Seller { get; set; }
    #endregion
}
