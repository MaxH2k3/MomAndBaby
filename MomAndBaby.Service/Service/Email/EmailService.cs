using FluentEmail.Core;
using MomAndBaby.BusinessObject.Models.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private static readonly string CachedTemplate;

        static EmailService()
        {
            // Xây dựng đường dẫn tuyệt đối tới tệp VerifyWithOTP.cshtml
            var projectRootPath = Directory.GetCurrentDirectory();
            var templatePath = Path.Combine(projectRootPath, "Pages", "Dashboard", "Components", "Common", "VerifyWithOTP.cshtml");

            // Đọc nội dung tệp vào CachedTemplate
            CachedTemplate = File.ReadAllText(templatePath);
        }

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
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
    }
}
