using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using MomAndBaby.Service.OrderService;

namespace MomAndBaby.Pages.Main.Body;

public class OrderDetail : PageModel
{
   
    private readonly IOrderService _orderService;
   

    public OrderDetail( IOrderService orderService)
    {
        _orderService = orderService;
    }

    public IEnumerable<OrderDetailResponseModel> listOrders { get; set; } = new List<OrderDetailResponseModel>();
    public OrderTracking orderTracking { get; set; } = new OrderTracking();
    public int? StatusOrder { get; set; }

    public async Task OnGet(int id)
    {
        orderTracking = await _orderService.GetOrderTracking(id);
        listOrders = await _orderService.GetAllOrderDetailOrder(id);
        StatusOrder = await _orderService.GetStatusOrder(id);
    }

    public async Task<IActionResult> OnPost(int id)
    {
        var result = await _orderService.CancelOrder(id, true);

        if (result)
        {
            TempData["Success"] = "Cancel order sucessful!";
        } else
        {
            TempData["Error"] = "Cancel order fail!";
        }

        return Redirect($"/order-detail?id={id}");
    }
}