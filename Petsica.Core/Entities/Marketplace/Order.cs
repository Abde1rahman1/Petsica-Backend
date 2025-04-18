﻿

namespace Petsica.Core.Entities.Marketplace;
public class Order
{
    public int OrderID { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = false;
    public string Address { get; set; }
    public bool IsCancelled { get; set; } = false;


    #region Foreign Key
    public string UserID { get; set; }
    public string SellerID { get; set; }

    #endregion

    #region Navigation Property
    public User User { get; set; }
    public User Seller { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    #endregion
}
