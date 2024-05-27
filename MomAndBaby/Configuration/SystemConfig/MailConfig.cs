using MomAndBaby.Models.SystemSetting;

namespace MomAndBaby.Configuration.SystemConfig
{
    public static class MailConfig
    {
        public static void AddFluentEmail(this IServiceCollection services, IConfiguration configuration)
        {
            var mailSetting = configuration.GetSection("GmailSetting").Get<GmailSetting>();

            services.AddFluentEmail(mailSetting.Mail)
            .AddSmtpSender(mailSetting.SmtpServer, mailSetting.Port,
                            mailSetting.DisplayName, mailSetting.Password)
            .AddRazorRenderer();
        }
    }
}
