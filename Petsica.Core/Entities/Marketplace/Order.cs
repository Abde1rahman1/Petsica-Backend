

namespace Petsica.Core.Entities.Marketplace;
public class Order
{
	public int OrderID { get; set; }
	public int Quantity { get; set; }
	public int TotalPrice { get; set; }

	#region Foreign Key
	public int UserID { get; set; }
	#endregion

	#region Navigation Property
	public User User { get; set; }
	#endregion
}
