using BertLunch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Stripe;

namespace BertLunch.Controllers
{
    [Route("payment")]
    public class PaymentController : Controller
    {
        private readonly BasketService _basketService;
        private readonly BertLunchContext _context;

        public PaymentController(BasketService basketService, BertLunchContext context)
        {
            _basketService = basketService;
            _context = context;
        }

        [HttpPost("stripe_payment")]
        public async Task<IActionResult> StripePayment(string stripeToken)
        {

            try
            {
                // Getting user email
                var userEmail = await _context.Users
                    .Where(x => x.UserName == User.Identity.Name)
                    .Select(x => x.Email)
                    .FirstOrDefaultAsync();

                var customers = new CustomerService();
                var charges = new ChargeService();

                // Create a customer
                var customerOptions = new CustomerCreateOptions
                {
                    Email = userEmail,
                    Source = stripeToken
                };
                var customer = await customers.CreateAsync(customerOptions);

                // Create metadata for the order
                var metadata = new Dictionary<string, string>();
                int itemCount = 1;
                foreach (var item in _basketService.GetBasket())
                {
                    metadata[$"item_{itemCount}_name"] = item.MenuName;
                    metadata[$"item_{itemCount}_quantity"] = item.Quantity.ToString();
                    itemCount++;
                }

                var totalAmount = _basketService.GetBasket().Sum(item => item.MenuPrice * item.Quantity);
                var totalAmountInCents = (long)(totalAmount * 100);


                // Inserting data into the stripe dashboard
                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = totalAmountInCents,
                    Currency = "eur",
                    Customer = customer.Id,
                    Description = "Charge for total order",
                    Metadata = metadata
                };

                var charge = await charges.CreateAsync(chargeOptions);
                if (charge.Status == "succeeded")
                {
                    // Payment was successful
                    Response.Cookies.Delete("cartItems");
                    return Redirect("~/PaymentPage/PaymentSuccessful");
                }
                else
                {
                    // Payment failed
                    return Redirect("~/PaymentPage/PaymentFailure"); 
                }
            }
            catch (StripeException ex)
            {
                // decline payment: 4000 0000 0000 0002
                return Redirect("~/PaymentPage/PaymentFailure");
                //switch (ex.StripeError.Type)
                //{
                //    case "card_error":
                //        return Redirect("~/PaymentPage/PaymentFailure");

                //    case "api_connection_error":
                //    case "api_error":
                //        return Redirect("~/PaymentPage/PaymentFailure");

                //    default:
                //        return Redirect("~/PaymentPage/PaymentFailure");
                //}
            }
        }
    }
}
