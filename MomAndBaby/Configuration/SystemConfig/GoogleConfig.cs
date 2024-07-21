using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;

namespace MomAndBaby.Configuration.SystemConfig
{
    public static class GoogleConfig
    {
        public static void AddGoogle(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {


                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
             {
                 options.ClientId = configuration.GetSection("GoogleKeys:ClientId").Value;
                 options.ClientSecret = configuration.GetSection("GoogleKeys:ClientSecret").Value;
                 //options.ClaimActions.MapJsonKey
                 options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "sub");
                 options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                 options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                 options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                 options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                 options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
             });
        }
    }
}
