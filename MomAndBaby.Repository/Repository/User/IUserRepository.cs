using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.UserDto;

namespace MomAndBaby.Repository
{
    public interface IUserRepository
    {
		Task<bool> IsStaff(Guid userId);
        Task<User?> GetUserByUsernameOrEmail(string userSelection);
        Task<User?> GetUserByEmail(string email);
        Task<User> AddUser(User userRegitser);
        Task<User> AddUserLoginGG(User userRegitser);
        Task<User> UpdateUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<User>> GetUsersExceptAdmin();
        Task<User> UpdateStatus(User user);
		Task<User> getUserById(Guid? id);
        Task<User?> GetUserById(Guid? id);
        Task<int> GetTotalUser();
    }
        
        
    
}
