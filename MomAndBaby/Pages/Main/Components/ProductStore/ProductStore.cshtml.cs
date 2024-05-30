using Microsoft.AspNetCore.Mvc;
using MomAndBaby.Models.ProductModel;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Components
{

    public class ProductStoreViewComponent : ViewComponent
    {
        private readonly IProductService _iproductService;

        public ProductStoreViewComponent(IProductService iproductService)
        {
            _iproductService = iproductService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ProductStoreModel productStoreModel = new ProductStoreModel();
            productStoreModel.ListAllProducts = await _iproductService.GetAll();
            productStoreModel.ListNewItems = await _iproductService.GetNewItems();
            productStoreModel.ListHighestRatingItems = await _iproductService.GetHighestRating();


            return View("ProductStore", productStoreModel);
        }
        
    }
}
