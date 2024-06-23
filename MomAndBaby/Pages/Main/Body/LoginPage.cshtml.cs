using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body
{
    public class LoginPageModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginPageModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostLoginGoogle()
        {
            //Console.WriteLine("AAA");
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {

                RedirectUri = Url.Page("GoogleResponse")

            });
            return new EmptyResult();

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

        public async Task<IActionResult> OnPostLogin()
        {
            string usernameValue = Request.Form["user-name"];
            string passwordValue = Request.Form["user-password"];
            await _userService.Login(usernameValue, passwordValue);
            return Redirect("/");
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/login");
        }
    }
}
