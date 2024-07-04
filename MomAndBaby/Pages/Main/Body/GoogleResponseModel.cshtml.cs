using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Service;
using System.Security.Claims;

namespace MomAndBaby.Pages.Main.Body
{
    public class GoogleResponseModel : PageModel
    {
        private readonly IUserService _userService;

        public GoogleResponseModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            var emailClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var nameClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var googleIdClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            if (emailClaim != null)
            {
                var user = await _userService.GetUserByEmail(emailClaim);
                if (user == null)
                {
                    user = new User
                    {
                        Email = emailClaim,
                        Username = nameClaim,
                        Id = Guid.Parse(googleIdClaim)
                    };
                    await _userService.AddNewUserGoogle(user);
                }
            }

            //return RedirectToPage("/Main/Body/HomePage");
            return Redirect("/");
        }
    }
}
