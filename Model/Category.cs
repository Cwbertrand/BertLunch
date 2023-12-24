namespace Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
