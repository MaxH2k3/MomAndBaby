using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Service;
using MomAndBaby.Service.OrderService;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class AccountDetailModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public AccountDetailModel(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        public User User { get; set; }
        public int NumOfOrders {get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<string> ShippingAddress { get; set; }
        public async Task<IActionResult> OnGet(Guid? id)
        {
           
            if (id == null)
            {
                ViewData["Message"] = "Id not found";
                return Page();
            }
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                ViewData["Message"] = "User not found";
                return Page();
            }

            var listAddress = await _orderService.GetShippingAddress(user.Id);
            var orders = await _orderService.GetAllOrder(user.Id);
            
            User = user;
            ShippingAddress = listAddress!;
            NumOfOrders = orders.Count();
            TotalAmount = await _orderService.GetTotalAmount(user.Id);
            return Page();
        }
    }
}
