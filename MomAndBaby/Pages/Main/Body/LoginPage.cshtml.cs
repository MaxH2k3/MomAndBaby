using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using System.Security.Claims;

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
           
            string usernameValue = Request.Form["username"]!;
            string emailValue = Request.Form["email"]!;
            string passwordValue = Request.Form["password"]!;
            var loginUserDto = new LoginUserDto(usernameValue, emailValue, passwordValue);
           
            await _userService.AddNewUser(loginUserDto);
            return Redirect("/");
        }

        public async Task<IActionResult> OnPostLogin()
        {
            string usernameValue = Request.Form["user-name"]!;
            string passwordValue = Request.Form["user-password"]!;
            var user = await _userService.Login(usernameValue, passwordValue);

            if(user == null)
            {
                return Page();
            }

            var claims = new List<Claim>
                {
                    new(UserClaimType.UserId, user.Id.ToString()),
                    new(UserClaimType.UserName, user.Username!),
                    new(UserClaimType.DisplayName, user.FullName!),
                    new(UserClaimType.Role, user.RoleId.ToString()!)
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),

                IsPersistent = true,

                IssuedUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Redirect("/");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/login");
        }
    }
}
