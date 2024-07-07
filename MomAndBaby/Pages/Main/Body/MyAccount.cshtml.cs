using FluentEmail.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.UserDto;
using MomAndBaby.Service;
using MomAndBaby.Utilities.Helper;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MomAndBaby.Pages.Main.Body
{
    public class MyAccountModel : PageModel
    {
        private readonly IUserService _userService;

        public MyAccountModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string? DisplayName { get; set; }

        [BindProperty]
        public string? FullName { get; set; }
       

        [BindProperty]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [PasswordMatch("NewPassword", ErrorMessage = "The passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        public async Task OnGet()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == UserClaimType.Email).Value;

            if (email != null)
            {
                var user = await _userService.GetUserByEmail(email);
                if (user != null)
                {
                    Email = user.Email;
                    DisplayName = user.Username;
                    FullName = user.FullName;
                    
                }
            }
        }

        public async Task<IActionResult> OnPostSaveAccount()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == UserClaimType.Email).Value;
            var updateUser = new UpdateUserDto(DisplayName, FullName, NewPassword);
            var userUpdatedEntity = await _userService.UpdateUser(email, updateUser);
            DisplayName = userUpdatedEntity.Username;
            FullName = userUpdatedEntity.FullName;
            TempData["MyAccount"] = "Update Account Detail Successfully";

            return Page();
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
