using BertLunch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Data;
using System.Globalization;

namespace BertLunch.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly BasketService _basketService;
        private readonly BertLunchContext _context;

        public OrderController(BasketService basketService, BertLunchContext context)
        {
            _basketService = basketService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("createOrder")]
        public IActionResult CreateOrder()
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user == null)  NotFound();


                var cartItems = _basketService.GetBasket();
                var itemsOrdered = new List<OrderItem>();
                string datetimeString = Request.Form["selectedTimeSlot"];
                string selectedDateString = Request.Form["selectedDate"];


                // Merging the date and the time selected from the form
                DateTime scheduledDate = DateTime.ParseExact(selectedDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                TimeSpan timeSlot = TimeSpan.Parse(datetimeString);
                DateTime scheduledDateTime = scheduledDate.Add(timeSlot);

                foreach (var item in cartItems)
                {
                    var productItem = _context.MenuItem
                        .Include(x => x.MenuCategory)
                        .Include(x => x.Category)
                        .FirstOrDefault(x => x.Id == item.ProductId);

                    if (productItem == null) return NotFound();

                    var menuOrderHistory = new MenuOrderHistory
                    {
                        Id = item.ProductId,
                        MenuOrderName = productItem.MenuName,
                        Image = productItem.Image,
                        CategoryName = productItem.Category.Label,
                        MenuCategory = productItem.MenuCategory.Name, 
                    };

                    var orderItem = new OrderItem
                    {
                        MenuOrderHistories = menuOrderHistory,
                        Quantity = item.Quantity,
                        Price = item.MenuPrice
                    };
                    itemsOrdered.Add(orderItem);
                }

                var subtotal = itemsOrdered.Sum(item => item.Price * item.Quantity);
                var reservation = new Reservation
                {
                    OrderItems = itemsOrdered,
                    Subtotal = subtotal,
                    User = user,
                    ReservationDate = scheduledDateTime
                };
                _context.Reservations.Add(reservation);
                //_context.SaveChanges();
                return Redirect("~/PaymentPage/Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

    
}
