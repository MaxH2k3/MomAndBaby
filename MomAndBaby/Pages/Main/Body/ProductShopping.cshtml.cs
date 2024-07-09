using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Service;
using Newtonsoft.Json;

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
        public int TotalProductsCount { get; set; }
        public int FilteredProductsCount { get; set; }

        public async Task OnGet()
        {
            Products = await _productService.GetAll();
            TotalProductsCount = Products.Count();
            TotalProductsCount = Products.Count();
            FilteredProductsCount = TotalProductsCount;
        }

        public IActionResult OnGetFilterProducts(decimal? startPrice, decimal? endPrice, int? numOfStars, string? sortCriteria)
        {
            var products = _productService.GetFilteredProducts(startPrice, endPrice, numOfStars, sortCriteria);
            TotalProductsCount = _productService.GetFilteredProducts(null, null, null, null).Count();


            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var productJson = JsonConvert.SerializeObject(products, jsonSettings);
            var response = new 
            {
                Products = productJson,
                TotalProductsCount,
                FilteredProductsCount = products.Count()

            };
            return new JsonResult(response);
        }
    }
}
