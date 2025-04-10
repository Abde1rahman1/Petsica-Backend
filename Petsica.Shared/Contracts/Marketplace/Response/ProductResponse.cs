using Petsica.Shared.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petsica.Shared.Contracts.Marketplace.Response
{
    public record ProductResponse
    (
	  int productId,
	  decimal Price,
	  decimal Discount,
	  string Description,
	  int Quantity,
	  string ProductName,
	  string Photo,
	  ProductCategory Category,
	  string SellerId,
	  bool IsAvailable
	);
}
