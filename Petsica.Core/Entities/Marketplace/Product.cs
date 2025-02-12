

namespace Petsica.Core.Entities.Marketplace;
public class Product
{
    public int ProductID { get; set; }
    public string Status { get; set; }
    public int Price { get; set; }
    public int Discount { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; }

    #region Foreign Key
    public string SellerID { get; set; }

    #endregion Navigation Property
    public User Seller { get; set; }

}
