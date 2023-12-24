using System.Globalization;

namespace Model.DTO
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string MenuName { get; set; }
        public float MenuPrice { get; set; }
        public string? MenuImage { get; set; }
        public int TotalPrice { get; set; }

    }
}
