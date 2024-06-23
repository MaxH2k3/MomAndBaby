using Microsoft.AspNetCore.Mvc;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Main.Components.QuickView
{
    [ViewComponent(Name = "QuickView")]
    public class QuickViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Console.WriteLine("QuickViewViewComponent.Invoke() called");
            return View(ConstantPage.MainPage.QuickView);
        }
    }
}
