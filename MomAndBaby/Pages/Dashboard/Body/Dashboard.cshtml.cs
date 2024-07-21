using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Middleware;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.OrderService;
using MomAndBaby.Service.Service;
using MomAndBaby.Service.Worker;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class DashboardModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly NotificationWorker _notificationWorker;

        [BindProperty]
        public DashboardDTO DashboardDTO { get; set; } = new DashboardDTO();

        public DashboardModel(IVoucherService voucherService, IProductService productService, IOrderService orderService,
            NotificationWorker notificationWorker)
        {
            _voucherService = voucherService;
            _productService = productService;
            _orderService = orderService;
            _notificationWorker = notificationWorker;
        }

        public async Task OnGet()
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.Dashboard;

            DashboardDTO.Vouchers = await _voucherService.GetVouchers();
            DashboardDTO.Gifts = await _voucherService.GetGifts();
            DashboardDTO.StatisticCategory = await _productService.GetStatisticsProductCategory();
            DashboardDTO.TotalMoneys = await _orderService.GetTotalAmount();
        }

        public async Task<IActionResult> OnPostDeleteVoucher(int id)
        {
            var result = await _voucherService.DeleteVoucher(id);
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Voucher, NotificationType.Deleted);

            return Redirect("/dashboard");

        }
    }
}
