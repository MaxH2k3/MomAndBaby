using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public void OnGet(int id)
    {
       
        _orderService.GetAllOrderDetailOrder(id);
    }
}