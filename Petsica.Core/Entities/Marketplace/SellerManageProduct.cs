﻿

namespace Petsica.Core.Entities.Marketplace;
public class SellerManageProduct
{
    public int ProductID { get; set; }
    public string SellerID { get; set; }

    #region Navigation Properties
    public Product Product { get; set; }
    public User Seller { get; set; }
    #endregion
}
