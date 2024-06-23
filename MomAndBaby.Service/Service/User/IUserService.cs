using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Service
{
    public interface IUserService
    {
       Task<bool> Login(string userSelection, string password);
       Task<bool> AddNewUser(LoginUserDto loginUser);
       Task<User?> GetUserByEmail(string email);
        Task<bool> AddNewUserGoogle(User user);


    }
}
