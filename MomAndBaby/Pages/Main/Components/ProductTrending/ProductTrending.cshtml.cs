using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Models.ProductModel;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Components
{
    public class ProductTrendingViewComponent: ViewComponent
    {
        private readonly IProductService _iproductService;

        public ProductTrendingViewComponent(IProductService iproductService)
        {
            _iproductService = iproductService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProductStoreModel productStoreModel = new ProductStoreModel();
            productStoreModel.ListTrendingProduct = await _iproductService.GetTrendingItems();
            


            return View("ProductTrending", productStoreModel);
        }
    }
}
