using Microsoft.AspNetCore.Mvc;
using MomAndBaby.Models.ProductModel;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Components
{

    public class ProductStoreViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductStoreViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProductStoreModel productStoreModel = new ProductStoreModel();
            productStoreModel.ListAllProducts = await _productService.GetAll();
            productStoreModel.ListNewItems = await _productService.GetNewItems();
            productStoreModel.ListHighestRatingItems = await _productService.GetHighestRating();


            return View("ProductStore", productStoreModel);
        }
        
    }
}
