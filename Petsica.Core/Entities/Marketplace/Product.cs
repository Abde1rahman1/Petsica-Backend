

namespace Petsica.Core.Entities.Marketplace;
public class Product
{
    public int ProductID { get; set; }
    public bool IsAvailable { get; set; } = true;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public bool IsDeleted { get; set; } = false;

    #region Foreign Key
    public string SellerID { get; set; }
    #endregion

    #region Navigation Property
    public User Seller { get; set; }
    public ProductCategory Category { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    #endregion

    public enum ProductCategory
    {
        Food,
        Toys,
        Accessories,
        Healthcare
    }

}
