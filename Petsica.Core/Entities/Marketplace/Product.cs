

namespace Petsica.Core.Entities.Marketplace;
public class Product
{
    public int ProductID { get; set; }
    public string Status { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; }


    #region Foreign Key
    public string SellerID { get; set; }
    #endregion

    #region Navigation Property
    public User Seller { get; set; }
    public ProductCategory Category { get; set; }
    #endregion

    public enum ProductCategory
    {
        Food,
        Toys,
        Accessories,
        Healthcare
    }

}
