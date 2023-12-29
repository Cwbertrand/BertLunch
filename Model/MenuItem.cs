using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Model
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string Ingredient { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string? Image { get; set; }

        //[NotMapped]
        //public IFormFile ImageFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public bool IsWeek { get; set; }
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int MenuCategoryId { get; set; }
        public MenuCategory? MenuCategory { get; set; }
    }
}
