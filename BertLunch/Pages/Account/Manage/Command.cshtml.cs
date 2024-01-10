using BertLunch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Data;
using QuestPDF.Fluent;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace BertLunch.Pages.Account.Manage
{
    public class CommandModel : PageModel
    {
        public ICollection<Reservation> Reservations { get; set; }
        private readonly BertLunchContext _context;
        private readonly PdfGeneratorService _pdfGeneratorService;
        private readonly IWebHostEnvironment _env;

        private string _imagePath;

        //Pagination
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;


        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 2;


        [BindProperty(SupportsGet = true)]
        public int PageCount { get; set; }


        public CommandModel(
            BertLunchContext context, 
            PdfGeneratorService pdfGeneratorService, 
            IWebHostEnvironment env)
        {
            _context = context;
            _pdfGeneratorService = pdfGeneratorService;
            _env = env;
        }

        public async Task OnGetAsync()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            IQueryable<Reservation> query =  _context.Reservations
                .Include(x => x.OrderItems)
                .Where(x => x.UserId == user.Id && 
                      (x.PaymentStatus == PaymentStatus.PaidByCard || x.PaymentStatus == PaymentStatus.PaidByCash));

            var totalCount = query.Count();
            PageCount = (int)Math.Ceiling((double)totalCount / PageSize);

            // This sets the page to either the first page if pageIndex is less than 1
            // And if it's more than the TotalPages, it sets to the last page
            PageIndex = Math.Max(1, Math.Min(PageIndex, PageCount));
            query = query.Skip((PageIndex - 1) * PageSize).Take(PageSize);
            Reservations = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostGeneratePdfAsync(int reservationId)
        {

            _imagePath = Path.Combine(_env.WebRootPath, "img", "Logos", "transparent background", "logo.png");
            var reservation = await _context.Reservations
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(res => res.Id == reservationId);

            if(reservation == null)
            {
                return NotFound();
            }
            var invoiceDocument = new InvoiceDocument(reservation, _imagePath);
            byte[] pdfContent = invoiceDocument.GeneratePdf();

            return File(pdfContent, "application/pdf", $"Invoice-{reservation.CommandNumber}.pdf");
        }

        public async Task<IActionResult> OnPostReviewAsync(int rating, string comment)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            if (user == null) NotFound();
            var review = new Review
            {
                User = user,
                Comment = comment,
                Rating = rating
            };

            _context.Reviews.Add(review);
            _context.SaveChangesAsync();

            return Page();
        }
    }
}
