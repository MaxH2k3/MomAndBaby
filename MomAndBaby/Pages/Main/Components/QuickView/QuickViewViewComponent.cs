using Microsoft.AspNetCore.Mvc;

namespace MomAndBaby.Pages.Main.Components.QuickView
{
    [ViewComponent(Name = "QuickView")]
    public class QuickViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Console.WriteLine("QuickViewViewComponent.Invoke() called");
            return View("QuickView");
        }
    }
}
