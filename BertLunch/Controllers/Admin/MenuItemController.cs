using Model;
using Model.Data;
using Model.DTO;
using BertLunch.Services.FormValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace BertLunch.Controllers.Admin
{
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

        public IActionResult Index()
        {
            return View("../../Pages/Admin/MenuItem/MenuItemIndex");
        }

        [HttpGet]
        [Route("create/menu/item")]
        public async Task<IActionResult> CreateMenuItem()
        {
            //ViewBag.Categories = new SelectList(await _context.Category.ToListAsync(), "Id", "Label");
            //ViewBag.MenuCategories = new SelectList(await _context.MenuCategories.ToListAsync(), "Id", "Name");

            Categories = await _context.Category.ToListAsync();
            MenuCategories = await _context.MenuCategories.ToListAsync();

            

            return View("~/Pages/Admin/MenuItem/CreateMenuItem.cshtml");
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create/menu/item")]
        public async Task<IActionResult> CreateMenuItem(MenuItem menuItem)
        {

            // Instantiating the MenuItemValidation class
            MenuItemValidation menuItemValidation = new MenuItemValidation();
            ValidationResult results = menuItemValidation.Validate(menuItem);

            // Querying the database to get the id of the various Category and MenuCategoryId
            menuItem.Category = await _context.Category.FindAsync(menuItem.CategoryId);
            //menuItem.MenuCategory = await _context.MenuCategories.FindAsync(menuItem.MenuCategoryId);

            if (results.IsValid && ModelState.IsValid)
            {
                if (menuItem.ImageFile != null)
                {
                    // Creating a unique name for the image while including the image extension
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + menuItem.ImageFile.FileName;

                    // Creating the image path inside wwwroot using _webHostEnvironment
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/menuImages", uniqueFileName);

                    // This saves the file into the directory
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        // Resizing the image using SixLabors bundle
                        using (var image = Image.Load(menuItem.ImageFile.OpenReadStream()))
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
                        await menuItem.ImageFile.CopyToAsync(stream);
                    }

                    // It then allocates it to the Image attribute which is saved in the database
                    menuItem.Image = "img/menuImages/" + uniqueFileName;

                }

                int selectedCategoryId = menuItem.CategoryId;
                //int selectedMenuCategoryId = menuItem.MenuCategoryId;

                menuItem.CreatedAt = DateTime.UtcNow;
                menuItem.CategoryId = selectedCategoryId;
                //menuItem.MenuCategoryId = selectedMenuCategoryId;
                _context.Add(menuItem);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(CreateMenuItem));

            }
            // Model is invalid; return to the view with validation errors
            foreach (var failure in results.Errors)
            {
                ModelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }

            return View("~/Pages/Admin/MenuItem/CreateMenuItem.cshtml");
        }



    }
}
