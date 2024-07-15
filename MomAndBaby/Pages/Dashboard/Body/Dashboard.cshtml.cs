using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using MomAndBaby.Service.Service;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class DashboardModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IProductService _productService;

        [BindProperty]
        public DashboardDTO DashboardDTO { get; set; } = new DashboardDTO();

        public DashboardModel(IVoucherService voucherService, IProductService productService)
        {
            _voucherService = voucherService;
            _productService = productService;
        }

        public async Task OnGet()
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.Dashboard;

            DashboardDTO.Vouchers = await _voucherService.GetVouchers();
            DashboardDTO.Gifts = await _voucherService.GetGifts();
            DashboardDTO.StatisticCategory = await _productService.GetStatisticsProductCategory();
        }
    }
}
