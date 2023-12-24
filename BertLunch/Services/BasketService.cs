using Model.Data;
using Model.DTO;
    using Newtonsoft.Json;

    namespace BertLunch.Services
    {
        public class BasketService
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly BertLunchContext _context;

            // Constructor
            public BasketService(IHttpContextAccessor httpContextAccessor, BertLunchContext context)
            {
                    _httpContextAccessor = httpContextAccessor;
                    _context = context;
            }

            //Adding or creating a new basket
            public List<CartItem> AddToBasket(int productId)
            {
                List<CartItem> cartItems = GetBasket();

                var menuItem = _context.MenuItem.FirstOrDefault(x => x.Id == productId);
                var existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    if(menuItem != null)
                    {
                        cartItems.Add(new CartItem
                        {
                            ProductId = productId,
                            Quantity = 1,
                            MenuName = menuItem.MenuName,
                            MenuImage = menuItem.Image,
                            MenuPrice = menuItem.Price,
                        });
                    }
                    
                }

                SaveBasket(cartItems);
                return cartItems;
            }

            public List<CartItem> RemoveFromBasket(int productId)
            {
                List<CartItem> cartItems = GetBasket();
                var existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);

                if (existingItem != null)
                {
                    if (existingItem.Quantity > 1)
                    {
                        existingItem.Quantity--;
                    }
                    else
                    {
                        cartItems.Remove(existingItem);
                    }

                    SaveBasket(cartItems);
                }

                return cartItems;

            }

            public List<CartItem> DeleteBasket(int productId)
            {
                List<CartItem> cartItems = GetBasket();
                var existingItem = cartItems.FirstOrDefault(item => item.ProductId == productId);
                if (existingItem != null)
                {
                    cartItems.Remove(existingItem);
                    SaveBasket(cartItems);
                }
                return cartItems;
            }

            // Getting the entire basket
            public List<CartItem> GetBasket()
            {
                var cartItems = _httpContextAccessor.HttpContext.Request.Cookies["cartItems"];
                if (cartItems != null)
                {
                    // Deserialize the JSON string from the cookie to a list of CartItem
                    return JsonConvert.DeserializeObject<List<CartItem>>(cartItems);
                }

                return new List<CartItem>();
            }

            // Creating the basket and saving it to the cookie storage
            private void SaveBasket(List<CartItem> cartItems)
            {
                // Serialize the list of CartItems to a JSON string
                var cartItemsJson = JsonConvert.SerializeObject(cartItems);

                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };

                // Save the JSON to a cookie
                _httpContextAccessor.HttpContext.Response.Cookies.Append("cartItems", cartItemsJson, cookieOptions);
            }
        }
    }
