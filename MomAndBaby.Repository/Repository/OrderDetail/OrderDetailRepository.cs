using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly MomAndBabyContext _context;

        public OrderDetailRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
