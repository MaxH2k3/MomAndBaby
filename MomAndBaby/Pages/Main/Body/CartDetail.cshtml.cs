using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
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
    private readonly IProductService _productService;
    // private readonly string _baseUrl = "https://localhost:7076";


    private readonly string _baseUrl = "https://momandbaby.azurewebsites.net";
    public CartDetailModel(IPayPalService payPalService, IConfiguration configuration, IOrderService orderService, IProductService productService)
    {
        _payPalService = payPalService;
        _orderService = orderService;
        _productService = productService;
    }
    public List<CartSessionModel> CartItems { get; set; } = new List<CartSessionModel>();
    public decimal Subtotal { get; set; }

    [BindProperty]
    public decimal Total { get; set; }

    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string Address { get; set; }

    [BindProperty]
    public string Tinh { get; set; }

    [BindProperty]
    public string Quan { get; set; }

    [BindProperty]
    public string Phuong { get; set; }


    public Dictionary<Guid, int> Errors { get; set; } = new Dictionary<Guid, int>();

    public void OnGet()
    {
        if (!User.Identity!.IsAuthenticated)
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
        var itemToRemove = cart.Find(item => item.Id.Equals(productId));
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
            Subtotal += item.UnitPrice * item.NumberOfProduct;
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

        // check stock
        var carts = GetCart();
        var errors = await _productService.CheckStock(carts);

        if (errors.Any())
        {
            CartItems = carts;
            Errors = errors;
            return Page();
        }


        var address = Address;
        HttpContext.Session.SetString("Address", JsonConvert.SerializeObject(address));
        var sessionData = HttpContext.Session.GetString("Total");
        if (sessionData == null)
        {
            return Redirect("shopping");
        }
        if (JsonConvert.DeserializeObject<Decimal>(sessionData) <= 0)
        {
            return Redirect("shopping");
        }
        var total = JsonConvert.DeserializeObject<Decimal>(sessionData);
        var totalDollar = total / 23000;
        Console.WriteLine($"Total Amount: {totalDollar.ToString("N0")}");
        var payment = _payPalService.CreatePayment(_baseUrl, "sale", "USD", totalDollar.ToString("N0"), "Sample Payment");
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

            return Redirect("/cart-detail");
        }

        // Handle payment failure
        return Redirect("/payment-canceled");
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
            StatusId = (int)OrderStatus.Processing,
            OrderDate = DateTime.Now,
            ShippingAddress = address,
            PaymentMethod = "Paypal"

        };
        var orderSave = await _orderService.CreateOrder(order);
        var orderDetails = cartData.Select(cartItem => new MomAndBaby.BusinessObject.Entity.OrderDetail
        {
            OrderId = orderSave,
            ProductId = cartItem.Id,
            Quantity = cartItem.NumberOfProduct,
            Price = cartItem.UnitPrice
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

    public void OnPostUpdateQuantity(Guid productId, int quantity)
    {
        var carts = GetCart();
        if (carts != null)
        {
            if (carts.Any(carts => carts.Id.Equals(productId)))
            {
                carts.FirstOrDefault(carts => carts.Id.Equals(productId))!.NumberOfProduct = quantity;
                SaveCart(carts);
            }

        }
        var cart = HttpContext.Session.GetString("Cart");
        if (!string.IsNullOrEmpty(cart))
        {
            CartItems = JsonConvert.DeserializeObject<List<CartSessionModel>>(cart);
            CalculateTotals();
        }
    }
}