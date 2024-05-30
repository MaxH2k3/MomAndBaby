using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Models.ProductModel;

namespace MomAndBaby.Pages.Main.Components
{
    public class ProductTrendingViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("ProductTrending");


           
        }
        public void OnGet()
        {
        }
    }
}
