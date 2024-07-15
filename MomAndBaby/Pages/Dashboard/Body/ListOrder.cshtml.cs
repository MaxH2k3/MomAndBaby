using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListOrderModel : PageModel
    {
        public void OnGet()
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.OrderList;
        }
    }
}
