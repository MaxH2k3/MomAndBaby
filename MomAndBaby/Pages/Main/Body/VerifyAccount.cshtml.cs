using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Models.UserDto;
using MomAndBaby.Service;
using MomAndBaby.Service.MessageConstant;
using MomAndBaby.Utilities.Helper;
using System.ComponentModel.DataAnnotations;

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
        public string Email { get; set; }

        [BindProperty]
        public string Otp { get; set; }



        public async Task OnGet()
        {
           
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(UserClaimType.Email))!.Value;

            if (email != null)
            {
                var user = await _userService.GetUserByEmail(email);
                if (user != null)
                {
                    Email = user.Email;
                   
                }
            }
        }

        public async Task<IActionResult> OnPostRegisterAccount()
        {
            var newValidation = new ValidateOtpDTO(Email, Otp);

            var result = await _userService.ValidateOTP(newValidation);
            if (!result)
            {
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
