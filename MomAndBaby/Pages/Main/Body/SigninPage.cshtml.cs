using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using System.Security.Claims;

namespace MomAndBaby.Pages.Main.Body
{
    public class SigninPageModel : PageModel
    {
        private readonly IUserService _userService;
        public SigninPageModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnPostLoginGoogle()
        {
            //Console.WriteLine("AAA");
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {

               RedirectUri = "/gg"


            });

            


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
                    new(UserClaimType.FullName, user.FullName!),
                    new(UserClaimType.Email, user.Email),
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
