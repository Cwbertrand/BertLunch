using BertLunch.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;

namespace BertLunch.Controllers
{
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly BasketService _basketService;

        public BasketController(BasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPut("addtobasket/{Id}")]
        public IActionResult AddToBasket(int Id)
        {
            try
            {
                var cartItems = _basketService.AddToBasket(Id);
                return Json(cartItems);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("removefrombasket/{Id}")]
        public IActionResult RemoveFromBasket(int Id)
        {
            var cartItems = _basketService.RemoveFromBasket(Id);
            return Json(cartItems);
        }

        [HttpDelete("deletebasket/{id}")]
        public IActionResult DeleteBasket(int Id)
        {
            var cartItems = _basketService.DeleteBasket(Id);
            return Json(cartItems);
        }

        [HttpGet("product_summary")]
        public IActionResult TotalQuantity()
        {
            var basketItems = _basketService.GetBasket();
            int totalQuantity = basketItems.Sum(x => x.Quantity);
            float totalPrice = basketItems.Sum(item => item.MenuPrice * item.Quantity);

            return Json(new { totalQuantity, totalPrice });
        }


    }
}
