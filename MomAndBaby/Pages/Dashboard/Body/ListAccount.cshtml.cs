using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Service;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListAccountModel : PageModel
    {
        private readonly IUserService _userService;

        public ListAccountModel(IUserService userService)
        {
            _userService = userService;
        }


        [BindProperty]
        public IEnumerable<User> Users { get; set; }
        public async Task OnGetAsycn()
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.AccountList;
            Users = await _userService.GetAllUsers();
        }

        

    }
}
