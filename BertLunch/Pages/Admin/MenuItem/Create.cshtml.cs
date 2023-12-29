using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Model.Data;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace BertLunch.Pages.Admin.MenuItem
{
    public class CreateModel : PageModel
    {
        private readonly BertLunchContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(BertLunchContext context, IWebHostEnvironment webHostEnvironment)
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

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Label");
        ViewData["MenuCategoryId"] = new SelectList(_context.MenuCategories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Model.MenuItem MenuItem { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        //public async Task<IActionResult> OnPostAsync()
        //{
        //  if (!ModelState.IsValid || _context.MenuItem == null || MenuItem == null)
        //    {
        //        return Page();
        //    }

        //    if (MenuItem.ImageFile != null)
        //    {
        //        // Creating a unique name for the image while including the image extension
        //        string uniqueFileName = Guid.NewGuid().ToString() + "_" + MenuItem.ImageFile.FileName;

        //        // Creating the image path inside wwwroot using _webHostEnvironment
        //        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img/menuImages", uniqueFileName);

        //        // This saves the file into the directory
        //        using (var stream = new FileStream(imagePath, FileMode.Create))
        //        {
        //            // Resizing the image using SixLabors bundle
        //            using (var image = Image.Load(MenuItem.ImageFile.OpenReadStream()))
        //            {
        //                image.Mutate(x => x.Resize(new ResizeOptions()
        //                {
        //                    Size = new Size(400, 300),
        //                    Mode = ResizeMode.Max
        //                }));

        //                IImageEncoder encoder = GetEncoder(Path.GetExtension(uniqueFileName));

        //                // saving the resized image to the stream
        //                image.Save(stream, encoder);
        //            }
        //            //await MenuItem.ImageFile.CopyToAsync(stream);
        //        }

        //        // It then allocates it to the Image attribute which is saved in the database
        //        MenuItem.Image = "img/menuImages/" + uniqueFileName;

        //    }

        //    MenuItem.CreatedAt = DateTime.UtcNow;
        //    //_context.MenuItem.Add(MenuItem);
        //    //await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}
