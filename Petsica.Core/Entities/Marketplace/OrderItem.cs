using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Core.Entities.Marketplace
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }

        #region Foreign Key
        public int SellerOrderId { get; set; }
        public int ProductId { get; set; }

        #endregion

        #region Navigation Properties
        public SellerOrder SellerOrder { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
