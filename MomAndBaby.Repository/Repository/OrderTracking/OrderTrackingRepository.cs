using Microsoft.EntityFrameworkCore;
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

        public async Task CreateOrderTracking(OrderTracking orderTracking)
        {
            var result = await _context.OrderTrackings.AddAsync(orderTracking);
            await _context.SaveChangesAsync();

        }

        public async Task<OrderTracking> GetOrderTrackingAsync(int orderId)
        {
            return await _context.OrderTrackings.Where(x=>x.OrderId == orderId).FirstOrDefaultAsync();
        }

        public async Task<bool> CancelOrder(int orderId)
        {
            return await _context.OrderTrackings.AnyAsync(x => x.OrderId.Equals(orderId) && (x.Delivery != null));
        }

    }
}
