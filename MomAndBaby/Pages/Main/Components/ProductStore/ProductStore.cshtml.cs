using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Entity;
using MomAndBaby.Repository;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Components.ProductStore
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
            var products = await _iproductService.GetAll();
            return View("ProductStore", products);
        }
        
    }
}
