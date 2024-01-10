using BertLunch.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model;
using Model.DTO;

namespace BertLunch.Pages.Basket
{
    
    
    public class BasketModel : PageModel
    {
        private readonly BasketService _basketService;
        private readonly BertLunchContext _context;
        public List<CartItem> BasketItems { get; set; }

        public BasketModel(BasketService basketService, BertLunchContext context)
        {
            _basketService = basketService;
            _context = context;
        }
        public void OnGet()
        {
            BasketItems = _basketService.GetBasket();
        }

        //public IActionResult OnPost()
        //{
        //    try
        //    {
        //        var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
        //        if (user == null) return NotFound();

        //        var cartItems = _basketService.GetBasket();
        //        var itemsOrdered = new List<OrderItem>();
        //        string datetimeString = Request.Form["selectedTimeSlot"];
        //        string selectedDatestring = Request.Form["selectedDate"];
        //        DateTime reservedTime;
        //        DateTime.TryParse(datetimeString, out reservedTime);
        //        foreach (var item in cartItems)
        //        {
        //            var productItem = _context.MenuItem
        //                .Include(x => x.MenuCategory)
        //                .Include(x => x.Category)
        //                .FirstOrDefault(x => x.Id == item.ProductId);

        //            if (productItem == null) return NotFound();

        //            var menuOrderHistory = new MenuOrderHistory
        //            {
        //                Id = item.ProductId,
        //                MenuOrderName = productItem.MenuName,
        //                Image = productItem.Image,
        //                CategoryName = productItem.Category.Label,
        //                MenuCategory = productItem.MenuCategory.Name,
        //            };

        //            var orderItem = new OrderItem
        //            {
        //                MenuOrderHistories = menuOrderHistory,
        //                Quantity = item.Quantity,
        //                Price = item.MenuPrice
        //            };
        //            itemsOrdered.Add(orderItem);
        //        }

        //        var subtotal = itemsOrdered.Sum(item => item.Price * item.Quantity);
        //        var reservation = new Reservation
        //        {
        //            OrderItems = itemsOrdered,
        //            Subtotal = subtotal,
        //            User = user,
        //            ReservationDate = reservedTime
        //        };
        //        _context.Reservations.Add(reservation);
        //        //_context.SaveChanges();
        //        return RedirectToPage("/PaymentPage/Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it appropriately
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}

        public IActionResult OnPostDelete(int id)
        {
            _basketService.DeleteBasket(id);

            return Redirect("/");
        }
    }
}
