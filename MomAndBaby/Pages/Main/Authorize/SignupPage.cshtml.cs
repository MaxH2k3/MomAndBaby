using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using MomAndBaby.Service.MessageConstant;
using MomAndBaby.Utilities.Helper;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MomAndBaby.Pages.Main.Authorize
{
    public class SignupPageModel : PageModel
    {
        private readonly IUserService _userService;
        public SignupPageModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$&*]).{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter and one special character.")]
        public string Password { get; set; }


        




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
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, return the page to display validation errors
                return Page();
            }

            var loginUserDto = new LoginUserDto(UserName, Email, Password);

            var result = await _userService.AddNewUser(loginUserDto);
            if (result == null)
            {
                TempData["MessageRegister"] = MessageAuthen.ExistedEmail;
                return Page();
            }
            var claims = new List<Claim>
            {
                    new(UserClaimType.UserId, result.Id.ToString()),
                    new(UserClaimType.UserName, result.Username!),
                    new(UserClaimType.FullName, result.FullName!),
                    new(UserClaimType.Email, result.Email),
                    new(UserClaimType.Role, result.RoleId.ToString()!)
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
    }
}
