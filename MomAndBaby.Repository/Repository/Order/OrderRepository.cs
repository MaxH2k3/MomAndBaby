using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MomAndBabyContext _context;

        public OrderRepository(MomAndBabyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return await _context.Orders.Include(x=>x.Status).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return _context.Orders.Where(o => o.Id == id).FirstOrDefault();
        }
    }
}
