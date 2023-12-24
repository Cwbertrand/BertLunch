using Model;
using Model.Data;

namespace BertLunch.Services
{
    public class SeedingData
    {
        public static async Task Seed(BertLunchContext context)
        {
            if (context.MenuItem.Any()) return;
            var category = new List<Category>
            {
                new Category
                {
                    Label = "La bière"
                },
                new Category
                {
                    Label = "Le jus"
                },
                new Category
                {
                    Label = "Le vin"
                },
                new Category
                {
                    Label = "Le poulet"
                },
                new Category
                {
                    Label = "Le bœuf"
                }
                ,
                new Category
                {
                    Label = "Végétalien"
                }

            };

            // Seeding Menu Category
            var menuCategory = new List<MenuCategory>
            {
                new MenuCategory
                {
                    Name = "Les assiettes"
                },
                new MenuCategory
                {
                    Name = "Les sandwiches"
                },
                new MenuCategory
                {
                    Name = "Les boissons"
                }

            };

            await context.Category.AddRangeAsync(category);
            await context.MenuCategories.AddRangeAsync(menuCategory);
            await context.SaveChangesAsync();
        }
    }
}
