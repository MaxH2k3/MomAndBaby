using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service.OrderService;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListOrderModel : PageModel
    {
        private readonly IOrderService _orderService;
        public ListOrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IEnumerable<MomAndBaby.BusinessObject.Models.OrderResponseModel> Orders { get; set; } = new List<MomAndBaby.BusinessObject.Models.OrderResponseModel>();

        public async void OnGet()
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.OrderList;
            Orders = await _orderService.GetAllOrderAdmin();
        }
    }
}
