using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using MomAndBaby.Service.Service;
using Newtonsoft.Json;

namespace MomAndBaby.Pages.Main.Body
{
    public class ProductShoppingModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductShoppingModel(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        
        }

        [BindProperty]
        public IEnumerable<Product> Products { get; set; }
        public int TotalProductsCount { get; set; }
        public int FilteredProductsCount { get; set; }
        
        public IEnumerable<ProductCategoryDto> ProductCategoryDto { get; set; }

       

        public async Task OnGet()
        {
            Products = await _productService.GetAll();
            TotalProductsCount = Products.Count();
            FilteredProductsCount = TotalProductsCount;

            ProductCategoryDto = await _productService.GetCategoryShopping();
        }

       
    }
}
