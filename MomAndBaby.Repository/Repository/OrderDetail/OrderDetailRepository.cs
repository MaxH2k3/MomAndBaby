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
    }
}
