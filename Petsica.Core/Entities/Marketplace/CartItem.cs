﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Core.Entities.Marketplace
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        #region Navigation Properties
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
