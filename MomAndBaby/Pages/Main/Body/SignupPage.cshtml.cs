using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using MomAndBaby.Utilities.Helper;

namespace MomAndBaby.Pages.Main.Body
{
    public class SignupPageModel : PageModel
    {
        private readonly IUserService _userService;
        public SignupPageModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.IsAuthenticated())
            {
                return Redirect("/");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRegisterAccount()
        {

            string usernameValue = Request.Form["username"];
            string emailValue = Request.Form["email"];
            string passwordValue = Request.Form["password"];
            var loginUserDto = new LoginUserDto(usernameValue, emailValue, passwordValue);

            var result = await _userService.AddNewUser(loginUserDto);
            if (!result)
            {
                TempData["MessageRegister"] = "Email has been already used for signing up";
                return Redirect("/register");
            }
            return Redirect("/");
        }
    }
}
