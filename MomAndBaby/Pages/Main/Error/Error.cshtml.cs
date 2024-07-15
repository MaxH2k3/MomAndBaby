using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Service.Extension;
using System.Net;

namespace MomAndBaby.Pages.Main.Error
{
    public class ErrorModel : PageModel
    {
        public IActionResult OnGet(int code)
        {
            if (User.Identity!.IsAuthenticated)
            {
                if (User.GetUserRoleFromToken() == (int)RoleType.Admin)
                {
                    if (code == (int)HttpStatusCode.NotFound || code == (int)HttpStatusCode.Forbidden)
                        return Redirect("/dashboard/error/notfound");
                    else if (code == (int)HttpStatusCode.Unauthorized)
                        return Redirect("/dashboard/error/timeout");
                    
                    return Redirect("/dashboard/error/internalserver");
                }
            }


            return Redirect("/error/notfound");
        }
    }
}
