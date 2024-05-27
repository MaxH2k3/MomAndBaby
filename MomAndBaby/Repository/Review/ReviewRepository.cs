using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MomAndBabyContext _context;

        public ReviewRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
