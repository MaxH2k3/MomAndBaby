using Microsoft.AspNetCore.Mvc;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.UserDto;

namespace MomAndBaby.Service
{
    public interface IUserService
    {
        Task<User?> Login(string userSelection, string password);
        Task<User?> AddNewUser(LoginUserDto loginUser);
        Task<User?> GetUserByEmail(string email);
        Task<bool> SigninGoogle(User user);
        Task<User> UpdateUser(string email, UpdateUserDto updateUserDto);
        Task<bool> GenerateAndSendOTP(string email, string userName, Guid userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> UpdateStatus(User user);

        Task<bool> ValidateOTP(ValidateOtpDTO validateOtp);
        Task<bool> ProcessValidOTP(UserValidation userValidation);
    }  
}
