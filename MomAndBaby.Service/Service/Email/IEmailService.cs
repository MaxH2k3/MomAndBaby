using MomAndBaby.BusinessObject.Models.UserDto;

namespace MomAndBaby.Service.Service.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailWithTemplate(string title, UserMailDto userMail);
        Task<bool> SendEmailAddStaff(string title, string email);
    }
}
