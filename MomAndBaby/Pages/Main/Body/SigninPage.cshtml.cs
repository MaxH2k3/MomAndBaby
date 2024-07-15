using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using MomAndBaby.Utilities.Helper;
using System.ComponentModel.DataAnnotations;
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
        [BindProperty]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$&*]).{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter and one special character.")]
        public string Password { get; set; }

        public async Task OnPostLoginGoogle()
        {
            //Console.WriteLine("AAA");
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {

               RedirectUri = "/gg"


            });

            


        }  
        
        public IActionResult OnGet()
        {
            if (HttpContext.Session.IsAuthenticated())
            {
                return Redirect("/");
            }
            return Page();
        }


        public async Task<IActionResult> OnPostLogin()
        {
            
            var user = await _userService.Login(UserName, Password);

            if(user == null)
            {
                return Page();
            } 

            //if(user.RoleId == 2)
            //{
            //    HttpContext.Session.SignIn(user);

            //} else if(user.RoleId == 4) 
            //{
            //    HttpContext.Session.SignIn(user, true);
            //}

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

            return Redirect("/verify");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            //HttpContext.Session.SignOut();
            return Redirect("/login");
        }
    }
}
