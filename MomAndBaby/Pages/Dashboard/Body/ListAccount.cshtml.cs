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


       
        public IEnumerable<User?> Users { get; set; }
       
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public async Task OnGet(int? pageIndex)
        {
            int pageSize = 4;
            CurrentPage = pageIndex ?? 1;

            var list  = await _userService.GetAllUsers();
            int count = list.Count();
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Users = list.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList();
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.AccountList;
           
        }

        public async Task<IActionResult> OnPostUpdateStatus(string userEmail, int i)
        {
            var user = await _userService.GetUserByEmail(userEmail);
            if (user != null)
            {
                await _userService.UpdateStatus(user);
            }
            await OnGet(i);
            return Page();
        }

        

    }
}
