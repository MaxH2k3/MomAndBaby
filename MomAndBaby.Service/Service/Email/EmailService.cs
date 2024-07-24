using FluentEmail.Core;
using Microsoft.Extensions.Configuration;
using MomAndBaby.BusinessObject.Models.UserDto;

namespace MomAndBaby.Service.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private static readonly string CachedTemplate;
        private static readonly string CachedTemplateStaff;
        private readonly IConfiguration _configuration;
        

        static EmailService()
        {
            
            var projectRootPath = Directory.GetCurrentDirectory();
            var templatePath = Path.Combine(projectRootPath, "Pages", "Dashboard", "Components", "Common", "VerifyWithOTP.cshtml");
            var templatePathStaff = Path.Combine(projectRootPath, "Pages", "Dashboard", "Components", "Common", "AddNewStaff.cshtml");

            
            CachedTemplate = File.ReadAllText(templatePath);
            CachedTemplateStaff = File.ReadAllText(templatePathStaff);
        }

        public EmailService(IFluentEmail fluentEmail, IConfiguration configuration)
        {
            _fluentEmail = fluentEmail;
            _configuration = configuration;
        }


        public async Task<bool> SendEmailWithTemplate(string title, UserMailDto userMail)
        {
            var renderedTemplate = CachedTemplate.Replace("@Model.UserName", userMail.UserName)
            .Replace("@Model.OTP", userMail.OTP);
            var response = await _fluentEmail.To(userMail.Email)
                                         .Subject(title)
                                         .Body(renderedTemplate, isHtml: true)
                                         .SendAsync();
            return response.Successful;
        }

        public async Task<bool> SendEmailAddStaff(string title, string email)
        {
            var renderedTemplate = CachedTemplateStaff.Replace("@Model.Url", _configuration["Url:BaseUrl"])
            .Replace("@Model.Email", email);
            var response = await _fluentEmail.To(email)
                                         .Subject(title)
                                         .Body(renderedTemplate, isHtml: true)
                                         .SendAsync();
            return response.Successful;
        }
    }
}
