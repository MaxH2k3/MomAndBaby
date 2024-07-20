using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Service;
using MomAndBaby.Utilities.Helper;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MomAndBaby.Pages.Main.Authorize
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

            if (user == null)
            {
                return Page();
            }
            TempData["Email"] = user.Email;
            TempData["Authen"] = JsonConvert.SerializeObject(user);



            return Redirect("/verify");
            // if (user.RoleId == (int)RoleType.Admin)
            // {
            //     return Redirect("/dashboard");
            // }

            // return Redirect("/");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("Total");
            await HttpContext.SignOutAsync();
            HttpContext.Session.SignOut();
            return Redirect("/login");
        }
    }
}
