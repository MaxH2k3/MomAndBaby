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

    }
}
