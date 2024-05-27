using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MomAndBabyContext _context;

        public UserRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
