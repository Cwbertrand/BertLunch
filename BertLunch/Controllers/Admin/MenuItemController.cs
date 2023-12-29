using Model;
using Model.Data;
using BertLunch.Services.FormValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace BertLunch.Controllers.Admin
{
    [Route("Admin/")]
    public class MenuItemController : Controller
    {
        public List<Category> Categories { get; set; }
        public List<MenuCategory> MenuCategories { get; set; }

        private readonly BertLunchContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MenuItemController(BertLunchContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create/menu/item")]
        public async Task<IActionResult> CreateMenuItem(MenuItem menuItem, IFormFile imageName)
        {

            // Instantiating the MenuItemValidation class
            MenuItemValidation menuItemValidation = new MenuItemValidation();
            ValidationResult results = menuItemValidation.Validate(menuItem);


            // Querying the database to get the id of the various Category and MenuCategoryId
            menuItem.Category = await _context.Category.FindAsync(menuItem.CategoryId);
            menuItem.MenuCategory = await _context.MenuCategories.FindAsync(menuItem.MenuCategoryId);

            if (results.IsValid && ModelState.IsValid)
            {
                TreatingPhoto(menuItem, imageName);
                menuItem.CreatedAt = DateTime.UtcNow;
                menuItem.CategoryId = menuItem.CategoryId;
                menuItem.MenuCategoryId = menuItem.MenuCategoryId;
                _context.Add(menuItem);

                await _context.SaveChangesAsync();
                return Json(new { success = true, redirectUrl = Url.Page("/Admin/MenuItem/Index") });

            }
            // Model is invalid; return to the view with validation errors
            foreach (var failure in results.Errors)
            {
                ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }

            return Json(new { success = false, redirectUrl = Url.Page("/Admin/MenuItem/Create") });
        }


        [HttpPost("edit/menu/item/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMenuItem(int id, MenuItem menuItem, IFormFile? editImageName)
        {
            MenuItemValidation menuItemValidation = new MenuItemValidation();
            ValidationResult results = menuItemValidation.Validate(menuItem);

            menuItem.Category = await _context.Category.FindAsync(menuItem.CategoryId);
            menuItem.MenuCategory = await _context.MenuCategories.FindAsync(menuItem.MenuCategoryId);

            if (results.IsValid && ModelState.IsValid)
            {
                menuItem.Id = id;
                if(editImageName != null)
                {
                    TreatingPhoto(menuItem, editImageName);
                }
                _context.Attach(menuItem).State = EntityState.Modified;
                _context.SaveChanges();
                return Json(new { success = true, redirectUrl = Url.Page("/Admin/MenuItem/Index") });
            }

            return Json(new { success = false, redirectUrl = Url.Page("/Admin/MenuItem/Edit/{id}") });
        }


        // Helper method to crop the image and save it to a directory
        private void TreatingPhoto(MenuItem menuItem, IFormFile imageName)
        {
            if (imageName != null && imageName.Length > 0)
            {
                // Creating a unique name for the image while including the image extension
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageName.FileName;

                // Creating the image path inside wwwroot using _webHostEnvironment
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/menuImages", uniqueFileName);

                // This saves the file into the directory
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    // Resizing the image using SixLabors bundle
                    using (var image = Image.Load(imageName.OpenReadStream()))
                    {
                        image.Mutate(x => x.Resize(new ResizeOptions()
                        {
                            Size = new Size(400, 300),
                            Mode = ResizeMode.Max
                        }));

                        IImageEncoder encoder = GetEncoder(Path.GetExtension(uniqueFileName));

                        // saving the resized image to the stream
                        image.Save(stream, encoder);
                    }
                    //await file.CopyToAsync(stream);
                }

                // It then allocates it to the Image attribute which is saved in the database
                menuItem.Image = "/img/menuImages/" + uniqueFileName;
            }
        }


        // Helper method to determine the image encoder based on the file extension
        private IImageEncoder GetEncoder(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".png":
                    return new PngEncoder();
                case ".jpg":
                case ".jpeg":
                    return new JpegEncoder();
                // Add more cases as needed for other formats
                default:
                    throw new NotSupportedException("Unsupported image format");
            }
        }
    }
}
