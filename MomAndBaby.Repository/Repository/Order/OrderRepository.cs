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

        public async Task<IEnumerable<Order>> GetAllOrder(Guid userId)
        {
            return await _context.Orders.Where(u=>u.UserId.Equals(userId)).Include(x=>x.Status).Include(z=>z.OrderTrackings).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return _context.Orders.Where(o => o.Id == id).Include(z=>z.OrderTrackings).FirstOrDefault();
        }

        public async Task UpdateAddress(string newAddress, int orderId)
        {
            Order order = await _context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            order.ShippingAddress = newAddress;
             _context.Orders.Update(order);
            
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailOrder(int orderId)
        {
            var orderDetail = await _context.OrderDetails.Where(x => x.OrderId == orderId)
                .Include(y=>y.Product)
                .Include(z=>z.Order)
                .ToListAsync();
            return orderDetail;
        }

        public async Task<int> CreateOrder(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
}
