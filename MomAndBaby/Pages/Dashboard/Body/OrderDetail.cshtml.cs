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
            await LoadData(id);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            await _orderService.ApproveOrder(id);
            await LoadData(id);
            return Page();
        }

        private async Task LoadData(int id)
        {
            orderId = id;
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.OrderDetail;
            orderTracking = await _orderService.GetOrderTracking(id);
            listOrders = await _orderService.GetAllOrderDetailOrder(id);
            Order = await _orderService.GetOrderById(id);
            user = await _userService.getUserById(Order.userId);
        }

        public async Task<IActionResult> OnPostApprovalOrder(int orderId)
        {
            await _orderService.ApprovalTracking(orderId);
           
            return Redirect($"/dashboard/order-detail?id={orderId}");
        }

        public async Task<IActionResult> OnPostCancelOrder(int orderId)
        {
            await _orderService.CancelOrder(orderId, false);

            return Redirect($"/dashboard/order-detail?id={orderId}");
        }


    }
}
