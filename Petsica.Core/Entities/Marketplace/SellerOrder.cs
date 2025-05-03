using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Core.Entities.Marketplace
{
    public class SellerOrder
    {
        public int SellerOrderId { get; set; }
        public bool Status { get; set; } = false;
        public bool IsCancelled { get; set; } = false ;
        public decimal TotalPrice { get; set; }

        #region Foreign Key
        public string SellerId { get; set; }
        public int OrderId { get; set; }
        #endregion

        #region Navigation Properties
        public Order Order { get; set; }
        public User Seller {  get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        #endregion
    }
}
