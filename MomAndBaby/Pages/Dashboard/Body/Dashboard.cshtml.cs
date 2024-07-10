using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
            TempData[VariableConstant.CurrentMenu] = (int)Menu.Dashboard;
        }
    }
}
