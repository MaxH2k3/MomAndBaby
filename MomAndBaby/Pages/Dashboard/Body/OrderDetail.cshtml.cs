using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using MomAndBaby.Service.OrderService;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        [BindProperty]
        public int orderId { get; set; }

        public OrderDetailModel(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public IEnumerable<OrderDetailResponseModel> listOrders { get; set; } = new List<OrderDetailResponseModel>();

        [BindProperty]
        public OrderTracking orderTracking { get; set; } = new OrderTracking();

        [BindProperty]
        public User user { get; set; }

        [BindProperty]
        public OrderResponseModel Order { get; set; } = new OrderResponseModel();
        public async Task OnGet(int id)
        {
            orderId = id;
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.OrderDetail;
            orderTracking = await _orderService.GetOrderTracking(id);
            listOrders = await _orderService.GetAllOrderDetailOrder(id);
            Order = await _orderService.GetOrderById(id);
            user = await _userService.getUserById(Order.userId);
        }
        
    }
}
