using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Models.ProductModel;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Components
{
    public class ProductTrendingViewComponent: ViewComponent
    {
        private readonly IProductService _productService;

        public ProductTrendingViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProductStoreModel productStoreModel = new ProductStoreModel();
            productStoreModel.ListTrendingProduct = await _productService.GetTrendingItems();
            


            return View("ProductTrending", productStoreModel);
        }
    }
}
