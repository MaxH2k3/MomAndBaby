using Microsoft.EntityFrameworkCore;
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

        public async Task CreateOrderDetail(List<OrderDetail> orderDetail)
        {
             await _context.OrderDetails.AddRangeAsync(orderDetail);
            
        }
        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailOrder(int orderId)
        {
            var orderDetail = await _context.OrderDetails.Where(x => x.OrderId == orderId)
                .Include(y => y.Product)
                .Include(z => z.Order)
                .ToListAsync();
            return orderDetail;
        }
    }
}
