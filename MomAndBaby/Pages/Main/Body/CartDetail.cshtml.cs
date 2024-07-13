using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.CartSessionModel;
using MomAndBaby.Service;
using MomAndBaby.Service.OrderService;
using MomAndBaby.Service.Service.PayPalService;
using Newtonsoft.Json;

namespace MomAndBaby.Pages.Main.Body;

public class CartDetailModel : PageModel
{

    private readonly IPayPalService _payPalService;
    private readonly string _baseUrl = "https://localhost:7076";
    public CartDetailModel(IPayPalService payPalService, IConfiguration configuration)
    {
        _payPalService = payPalService;
    }
    public List<CartSessionModel> CartItems { get; set; } = new List<CartSessionModel>();
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public void OnGet()
    {
        if (!User.Identity.IsAuthenticated)
        {
            Redirect("/login");
        }
        var cart = HttpContext.Session.GetString("Cart");
        if (!string.IsNullOrEmpty(cart))
        {
            CartItems = JsonConvert.DeserializeObject<List<CartSessionModel>>(cart);
            CalculateTotals();
        }
    }

    public IActionResult OnPostRemoveFromCart(Guid productId)
    {
        var cart = GetCart();
        var itemToRemove = cart.Find(item => item.Id == productId);
        if (itemToRemove != null)
        {
            cart.Remove(itemToRemove);
            SaveCart(cart);
            CalculateTotals();
        }
        return RedirectToPage();
    }

    private List<CartSessionModel> GetCart()
    {
        var cart = HttpContext.Session.GetString("Cart");
        if (string.IsNullOrEmpty(cart))
        {
            return new List<CartSessionModel>();
        }
        return JsonConvert.DeserializeObject<List<CartSessionModel>>(cart);
    }

    private void SaveCart(List<CartSessionModel> cart)
    {
        HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }

    private void CalculateTotals()
    {
        Subtotal = 0;
        foreach (var item in CartItems)
        {
            if (item.UnitPrice.HasValue)
            {
                Subtotal += item.UnitPrice.Value;
            }
        }
        Total = Subtotal;
        HttpContext.Session.SetString("Total", JsonConvert.SerializeObject(Total));
    }
    

    public IActionResult OnPostPay()
    {
        var sessionData = HttpContext.Session.GetString("Total");
        var total = JsonConvert.DeserializeObject<Decimal>(sessionData);
        Console.WriteLine($"Total Amount: {total.ToString("F2")}");
        var payment = _payPalService.CreatePayment(_baseUrl, "sale", "USD",total.ToString("F2"), "Sample Payment");

        var approvalUrl = payment.GetApprovalUrl();
        return Redirect(approvalUrl);
    }

    public  IActionResult OnGetPaymentSuccess(string paymentId, string token, string PayerID)
    {
        var payment = _payPalService.ExecutePayment(paymentId, PayerID);

        if (payment.state == "approved")
        {
            // Save the order details to the database
            // SaveOrderDetails();
            // Clear the cart after successful payment
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("Total");

            return RedirectToPage("OrderConfirmation");
        }

        // Handle payment failure
        return RedirectToPage("PaymentFailed");
    }

    // private void SaveOrderDetails()
    // {
    //     var order = new Order
    //     {
    //         UserId = User.Identity.Name, // Assuming you have a user identity setup
    //         TotalAmount = Total,
    //         OrderDate = DateTime.Now,
    //         OrderItems = CartItems.Select(cartItem => new OrderItem
    //         {
    //             ProductId = cartItem.Id,
    //             Quantity = 1, // Adjust based on your cart item structure
    //             UnitPrice = cartItem.UnitPrice.Value
    //         }).ToList()
    //     };

    //     _orderService.SaveOrder(order);
    // }
}