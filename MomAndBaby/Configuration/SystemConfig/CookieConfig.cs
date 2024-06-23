using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Configuration.SystemConfig
{
    public static class CookieConfig
{
    public static void AddCustomCookie(this IServiceCollection services, IConfiguration configuration)
    {
            services.Configure<CookieSetting>(configuration.GetSection("CookieSetting"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                var cookieSetting = configuration.GetSection("CookieSetting").Get<CookieSetting>();

                options.ExpireTimeSpan = TimeSpan.FromDays(cookieSetting.ExpireTime);
                options.Cookie.HttpOnly = cookieSetting.HttpOnly;
                options.Cookie.SecurePolicy = (CookieSecurePolicy)cookieSetting.SecurePolicy;
                options.Cookie.SameSite = (SameSiteMode)cookieSetting.SameSite;
                options.Cookie.Name = cookieSetting.Name;
                options.LoginPath = cookieSetting.LoginPath;
                options.LogoutPath = cookieSetting.LogoutPath;
                options.AccessDeniedPath = cookieSetting.AccessDeniedPath;

            });
            

            
    }
}
}
