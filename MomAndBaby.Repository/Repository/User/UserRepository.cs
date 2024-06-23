using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;

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
            await _context.Users.AddAsync(userRegitser);
            _context.SaveChanges();
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

    }
}
