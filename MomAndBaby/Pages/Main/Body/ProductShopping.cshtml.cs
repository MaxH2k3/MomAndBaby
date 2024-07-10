using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body
{
    public class ProductShoppingModel : PageModel
    {
        private readonly IProductService _productService;

        public ProductShoppingModel(IProductService productService)
        {
            _productService = productService;
        
        }

        [BindProperty]
        public IEnumerable<Product> Products { get; set; }

        public async Task OnGet()
        {
            Products = await _productService.GetAll();
            
        }

        public async Task<IActionResult> OnGetFilterProducts(decimal? startPrice, decimal? endPrice, int? numOfStars, string sortCriteria)
        {
            Products = await _productService.GetFilteredProducts(startPrice, endPrice, numOfStars, sortCriteria);
            return new JsonResult(Products); ;
        }
        public async Task<IActionResult> OnPostAddToCart(Guid productId)
        {
            var cart = HttpContext.Session.GetString("Cart");
            List<Guid> productIds = string.IsNullOrEmpty(cart) ? new List<Guid>() : System.Text.Json.JsonSerializer.Deserialize<List<Guid>>(cart);
            productIds.Add(productId);
            HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(productIds));

            return new JsonResult(new { success = true });
        }
        
        public async Task<IActionResult> OnGetCartDataAsync()
        {
            var cart = HttpContext.Session.GetString("Cart");
            List<Guid> productIds = string.IsNullOrEmpty(cart) ? new List<Guid>() : System.Text.Json.JsonSerializer.Deserialize<List<Guid>>(cart);

            var products = await _productService.GetProductsByIdsAsync(productIds);
        
            return new JsonResult(products);
        }

    }
}
