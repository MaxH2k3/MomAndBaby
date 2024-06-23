using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

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
             });
        }
    }
}
