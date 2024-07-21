using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using System.Security.Claims;
using MomAndBaby.Service;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Enums;

namespace MomAndBaby.Pages.Main.Body
{
    public class GoogleResponseModel : PageModel
    {
        private readonly IUserService _userService;

        public GoogleResponseModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGet()
        {
            

            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result?.Principal != null)
            {
                var identity = result.Principal.Identities.FirstOrDefault();
                if (identity != null)
                {
                    var claims = identity.Claims.Select(claim => new
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
                       
                            user = new User
                            {
                                Email = emailClaim,
                                Username = nameClaim,
                                FullName = nameClaim,
                                Id = Guid.NewGuid()
                            };
                            await _userService.SigninGoogle(user);
                            var existUser = await _userService.GetUserByEmail(emailClaim);
                            int roleId = existUser.RoleId != 0 ? existUser.RoleId : (int)RoleType.Customer;
                            var claimsList = new List<Claim>
                                {
                                    new(UserClaimType.UserId, user.Id.ToString()),
                                    new(UserClaimType.UserName, nameClaim),
                                    new(UserClaimType.FullName, nameClaim),
                                    new(UserClaimType.Email, emailClaim),
                                
                                    new(UserClaimType.Role, roleId.ToString())

                                };  
                            var claimsIdentity = new ClaimsIdentity(
                                    claimsList, CookieAuthenticationDefaults.AuthenticationScheme);
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

                            if (existUser.RoleId == (int)RoleType.Admin)
                            {
                                return Redirect("./dashboard");
                            }
                    }


                   
                    return Redirect("/");
                }
            }

            
            // Handle the case where authentication failed
            return Redirect("/Error");
        }
    }
}
