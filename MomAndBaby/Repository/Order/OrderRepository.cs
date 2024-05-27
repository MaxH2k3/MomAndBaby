using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MomAndBabyContext _context;

        public OrderRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
