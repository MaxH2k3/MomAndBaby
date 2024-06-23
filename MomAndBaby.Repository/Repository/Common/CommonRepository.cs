using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly MomAndBabyContext _context;

        public CommonRepository(MomAndBabyContext context)
        {
            _context = context;
        }

    }
}
