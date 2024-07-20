using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.UserDto;

namespace MomAndBaby.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MomAndBabyContext _context;

        public UserRepository(MomAndBabyContext context)
        {
            _context = context;
        }

        public async Task<bool> IsStaff(Guid userId)
        {
            return await _context.Users.AnyAsync(x => x.RoleId == (int)RoleType.Staff && x.Id.Equals(userId));
		}

        public async Task<User> AddUser(User userRegitser)
        {
            userRegitser.Status = "InActive";
            userRegitser.RoleId = (int)RoleType.Customer;
            await _context.Users.AddAsync(userRegitser);
            return userRegitser;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User?> GetUserByUsernameOrEmail(string userSelection)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(userSelection) || u.Email.Equals(userSelection));
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> UpdateStatus(User user)
        {
            if (user.Status.Equals(UserStatus.Active.ToString()))
            {
                user.Status = UserStatus.Banned.ToString();
            } else if (user.Status.Equals(UserStatus.Banned.ToString()))
            {
                user.Status=UserStatus.Active.ToString();
            }
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserById(Guid? id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

       
    }
}
