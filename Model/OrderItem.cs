namespace Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public MenuOrderHistory MenuOrderHistories { get; set; }
        public float Price { get; set; }
    }
}
