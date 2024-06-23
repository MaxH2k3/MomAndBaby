using MomAndBaby.Configuration.Uow;
using MomAndBaby.Dto.UserDto;
using MomAndBaby.Entity;

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
