using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.UserDto;
using MomAndBaby.Service;
using MomAndBaby.Service.MessageConstant;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MomAndBaby.Pages.Main.Body
{
    public class VerifyAccountModel : PageModel
    {
        private readonly IUserService _userService;
        public VerifyAccountModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string Otp { get; set; }



        public void OnGet()
        {
           
            
        }

        public async Task<IActionResult> OnPostRegisterAccount()
        {
            var newValidation = new ValidateOtpDTO(Email, Otp);
            Email = TempData["Email"].ToString();
            var result = await _userService.ValidateOTP(newValidation);
            if (!result)
            {
                var userJson = TempData["Authen"].ToString();
                User user = JsonConvert.DeserializeObject<User>(userJson);

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
            else
            {
                TempData["MessageVerify"] = MessageOtp.OTPUnvalid;
                return Page();
            }
        }
    }   
}
