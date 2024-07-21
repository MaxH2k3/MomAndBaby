using FluentEmail.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Service;
using MomAndBaby.Service.MessageConstant;
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
		public string UserName { get; set; }

        [BindProperty]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        public async Task OnPostLoginGoogle()
        {
            //Console.WriteLine("AAA");
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {

                RedirectUri = "/gg",
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30), // ??m b?o giá tr? h?p l?
				IsPersistent = true,
				AllowRefresh = true


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
            HttpContext.Session.SetString("Email", user.Email);

            if (user == null)
            {
				TempData["MessageSignin"] = MessageAuthen.InvalidSignin;
				return Page();
            }
            TempData["Email"] = user.Email;
            TempData["Authen"] = JsonConvert.SerializeObject(user);
            TempData["Email"] = JsonConvert.SerializeObject(user.Email);


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
