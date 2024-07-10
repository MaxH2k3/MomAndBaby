using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.CartSessionModel;
using MomAndBaby.Service;
using MomAndBaby.Service.OrderService;
using Newtonsoft.Json;

namespace MomAndBaby.Pages.Main.Body;

public class CartDetailModel : PageModel
{

 
        public List<CartSessionModel> CartItems { get; set; } = new List<CartSessionModel>();

        public void OnGet()
        {
            var cart = HttpContext.Session.GetString("Cart");
            
            if (!string.IsNullOrEmpty(cart))
            {
                Console.WriteLine("cart not empty");
                CartItems = JsonConvert.DeserializeObject<List<CartSessionModel>>(cart);
            }
        }
}