using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Service.Service.PayPalService;

namespace MomAndBaby.Pages.Main.Body
{
    public class CheckOutModel : PageModel
    {
        private readonly IPayPalService _payPalService;
        private readonly string _baseUrl;

        public CheckOutModel(IConfiguration configuration, IPayPalService payPalService)
        {
            _payPalService = payPalService;
            _baseUrl = "https://localhost:7076";
        }

        public IActionResult OnPostPay()
        {
            var payment = _payPalService.CreatePayment(_baseUrl, "sale", "USD", "10.00", "Sample Payment");

            var approvalUrl = payment.GetApprovalUrl();
            return Redirect(approvalUrl);
        }
    }
}
