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
        private static readonly string CachedTemplate = File.ReadAllText("./Pages/Authentication/Dashboard/Components/Common/VerifyWithOTP.cshtml");


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
