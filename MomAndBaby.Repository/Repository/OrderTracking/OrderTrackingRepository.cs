using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class OrderTrackingRepository : IOrderTrackingRepository
    {
        private readonly MomAndBabyContext _context;

        public OrderTrackingRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
