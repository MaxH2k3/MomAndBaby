using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.UserDto;
using MomAndBaby.Service;
using MomAndBaby.Service.Helper;
using MomAndBaby.Service.MessageConstant;
using MomAndBaby.Service.Service;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MomAndBaby.Pages.Main.Body
{
    public class VerifyAccountModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IUserValidationService _userValidationService;
        public VerifyAccountModel(IUserService userService, IUserValidationService userValidationService)
        {
            _userService = userService;
            _userValidationService = userValidationService;
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
            //var email = TempData["Email"].ToString();
            //Email = email.Trim(new char[] { '\\', '\"' });
            Email = HttpContext.Session.GetString("Email");
            var newValidation = new ValidateOtpDTO(Email, Otp);
            
            var result = await _userService.ValidateOTP(newValidation);
            if (result)
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
                if(user.RoleId == (int)RoleType.Admin)
                {
                    return Redirect("./dashboard");
                }
                return Redirect("/");
            }
            else
            {
                TempData["MessageVerify"] = MessageOtp.OTPUnvalid;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostResendOTP()
        {

            TempData.Remove("MessageVerify");
            Email = HttpContext.Session.GetString("Email");
            var user = await _userService.GetUserByEmail(Email!);
            var userValidation = await _userValidationService.GetUser(user!.Id);
            if(userValidation.ExpiredAt > TimeHelper.GetCurrentInVietName())
            {
                TempData["MessageResend"] = MessageOtp.OTPStillValid;
                return Page();
            }
            var result = await _userService.GenerateAndSendOTP(Email!, user!.Username, user.Id);
            if (result)
            {
                TempData["MessageResend"] = MessageOtp.ResendOTP;
                return Page();
            } else
            {
                TempData["MessageResend"] = MessageAuthen.InvalidSignin;
                return Page();
            }
        }
    }   
}
