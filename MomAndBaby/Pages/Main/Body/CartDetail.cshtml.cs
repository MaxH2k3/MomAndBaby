using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.CartSessionModel;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.OrderService;
using MomAndBaby.Service.Service.PayPalService;
using Newtonsoft.Json;

namespace MomAndBaby.Pages.Main.Body;

public class CartDetailModel : PageModel
{

    private readonly IPayPalService _payPalService;
    private readonly IOrderService _orderService;

    private readonly string _baseUrl = "https://localhost:7076";
    public CartDetailModel(IPayPalService payPalService, IConfiguration configuration, IOrderService orderService)
    {
        _payPalService = payPalService;
        _orderService = orderService;
    }
    public List<CartSessionModel> CartItems { get; set; } = new List<CartSessionModel>();
    public decimal Subtotal { get; set; }

    [BindProperty]
    public decimal Total { get; set; }

    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    [BindProperty]
    public string Address { get; set; }

    public void OnGet()
    {
        if(!User.Identity!.IsAuthenticated)
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


    public async Task<IActionResult> OnPostPay()
    {
        if (!ModelState.IsValid)
            {
                return Page();
            }
        HttpContext.Session.SetString("Address", JsonConvert.SerializeObject(Address));
        var sessionData = HttpContext.Session.GetString("Total");
        if (JsonConvert.DeserializeObject<Decimal>(sessionData) == 0)
        {
            return RedirectToPage();
        }
        var total = JsonConvert.DeserializeObject<Decimal>(sessionData);
        Console.WriteLine($"Total Amount: {total.ToString("F2")}");
        var payment = _payPalService.CreatePayment(_baseUrl, "sale", "USD", total.ToString("F2"), "Sample Payment");
        var approvalUrl = payment.GetApprovalUrl();
        return Redirect(approvalUrl);
    }

    public async Task<IActionResult> OnGetPaymentSuccess(string paymentId, string token, string PayerID)
    {
        var payment = _payPalService.ExecutePayment(paymentId, PayerID);

        if (payment.state == "approved")
        {
            // Save the order details to the database
            await SaveOrderDetails();
            // Clear the cart after successful payment
             HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("Total");

            return RedirectToPage("/cart-detail");
        }

        // Handle payment failure
        return RedirectToPage("PaymentFailed");
    }

    private async Task SaveOrderDetails()
    {
        var cart = HttpContext.Session.GetString("Cart");
         var cartData = JsonConvert.DeserializeObject<List<CartSessionModel>>(cart);
        var sessionData = HttpContext.Session.GetString("Total");
        // var firstNameData = HttpContext.Session.GetString("FirstName");
        // var lastNameData = HttpContext.Session.GetString("LastName");
        var addressData = HttpContext.Session.GetString("Address");
        var total = JsonConvert.DeserializeObject<Decimal>(sessionData);
        // var firstName = JsonConvert.DeserializeObject<Decimal>(firstNameData);
        // var lastName = JsonConvert.DeserializeObject<Decimal>(lastNameData);
        var address = JsonConvert.DeserializeObject<string>(addressData);


        var order = new Order
        {
            UserId = Guid.Parse(User.GetUserIdFromToken()), // Assuming you have a user identity setup
            TotalAmount = total,
            StatusId = 1,
            OrderDate = DateTime.Now,
            ShippingAddress = address,
            PaymentMethod = "Paypal"

        };
        var orderSave = await _orderService.CreateOrder(order);
        var orderDetails = cartData.Select(cartItem => new MomAndBaby.BusinessObject.Entity.OrderDetail
        {
            OrderId = orderSave,
            ProductId = cartItem.Id,
            Quantity = 1, 
            Price = cartItem.UnitPrice.Value
        }).ToList();

        // Create the order tracking
        var orderTracking = new OrderTracking
        {
            OrderId = orderSave,
            Delivery = DateTime.Now.AddDays(3),
            Complete = DateTime.Now.AddDays(7),
            Package = DateTime.Now.AddDays(1),
            OrderConfirmation = DateTime.Now.AddDays(1)
            // Add other necessary properties for tracking if required
        };

        // Save order details and complete the order
        await _orderService.CreateOrderDetail(orderDetails);
        await _orderService.CompleteOrder(orderTracking);

    }
}