using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MomAndBaby.Pages.Main.Body
{
    public class HomePageModel : PageModel
    {
        [Authorize]
        public void OnGet()
        {
        }
    }
}
