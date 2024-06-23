using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class GiftRepository : IGiftRepository
    {
        private readonly MomAndBabyContext _context;

        public GiftRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
