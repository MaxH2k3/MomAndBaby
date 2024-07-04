using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body
{
    public class SignupPageModel : PageModel
    {
        private readonly IUserService _userService;
        public SignupPageModel(IUserService userService)
        {
            _userService = userService;
        }
       

        public async Task<IActionResult> OnPostRegisterAccount()
        {

            string usernameValue = Request.Form["username"];
            string emailValue = Request.Form["email"];
            string passwordValue = Request.Form["password"];
            var loginUserDto = new LoginUserDto(usernameValue, emailValue, passwordValue);

            await _userService.AddNewUser(loginUserDto);
            return Redirect("/");
        }
    }
}
