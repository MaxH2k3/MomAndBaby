using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Entity;
using MomAndBaby.Repository;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Components
{
    
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductService _iproductService;

        public ProductViewComponent(IProductService iproductService)
        {
            _iproductService = iproductService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = _iproductService.GetAll();
            return View("Product", products);
        }
        
    }
}
