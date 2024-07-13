using MomAndBaby.BusinessObject.Models.UserDto;

namespace MomAndBaby.Service.Service.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailWithTemplate(string title, UserMailDto userMail);
    }
}
