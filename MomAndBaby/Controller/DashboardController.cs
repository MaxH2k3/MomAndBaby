using Microsoft.AspNetCore.Mvc;
using MomAndBaby.Service.OrderService;

namespace MomAndBaby.Controller
{
    [Route("api/v1/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders/{year}")]
        public async Task<IEnumerable<decimal>> GetTotalMoneyByYear(int year)
        {
            return await _orderService.GetTotalAmount(year);
        }

    }
}
