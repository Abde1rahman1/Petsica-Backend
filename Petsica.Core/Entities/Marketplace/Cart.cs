namespace Petsica.Core.Entities.Marketplace
{
    public class Cart
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; } = 0;

        #region Foreign Key
        public string UserID { get; set; } = string.Empty;
        #endregion

        #region Navigation Property
        public User User { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        #endregion
    }
}
