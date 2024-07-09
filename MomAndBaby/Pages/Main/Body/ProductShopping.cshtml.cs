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
            return Partial("ProductShopping", Products);
        }
    }
}
