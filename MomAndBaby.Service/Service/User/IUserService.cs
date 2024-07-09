using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.UserDto;

namespace MomAndBaby.Service
{
    public interface IUserService
    {
        Task<User?> Login(string userSelection, string password);
        Task<bool> AddNewUser(LoginUserDto loginUser);
        Task<User?> GetUserByEmail(string email);
        Task<bool> SigninGoogle(User user);
        Task<User> UpdateUser(string email, UpdateUserDto updateUserDto);

    }
}
