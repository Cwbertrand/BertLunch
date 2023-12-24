using Microsoft.EntityFrameworkCore;

namespace Model
{
    [Owned]
    public class MenuOrderHistory
    {
        public int Id { get; set; }
        public string MenuOrderName {  get; set; }
        public string CategoryName { get; set; }
        public string MenuCategory {  get; set; }
        public string Image {  get; set; }
    }
}
