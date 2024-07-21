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
            return await _context.Orders.Where(u => u.UserId.Equals(userId)).Include(x => x.Status).Include(z => z.OrderTrackings).ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return _context.Orders.Where(o => o.Id == id).Include(x => x.Status).Include(z => z.OrderTrackings).FirstOrDefault();
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
                .Include(y => y.Product)
                .Include(z => z.Order)
                .ToListAsync();
            return orderDetail;
        }

        public async Task<int> CreateOrder(Order order)
        {
            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<IEnumerable<decimal>> GetTotalMoneyByYear(int year)
        {
            var orders = _context.Orders.Where(o => o.OrderDate!.Value.Year == year).AsQueryable();
            var totalYear = new List<decimal>();
            for (int month = 1; month <= 12; month++)
            {
                totalYear.Add(await orders.Where(o => o.OrderDate!.Value.Month == month).SumAsync(o => o.TotalAmount));
            }

            return totalYear;
        }

        public async Task<IEnumerable<IEnumerable<decimal>>> GetTotalMoney()
        {
            var orders = _context.Orders.AsQueryable();
            var thisYear = new List<decimal>();
            var lastYear = new List<decimal>();
            var totalYear = new List<decimal>();

            for (int month = 1; month <= 12; month++)
            {
                thisYear.Add(await orders.Where(o => o.OrderDate!.Value.Year == DateTime.Now.Year && o.OrderDate!.Value.Month == month).SumAsync(o => o.TotalAmount));
                lastYear.Add(await orders.Where(o => o.OrderDate!.Value.Year == DateTime.Now.Year - 1 && o.OrderDate!.Value.Month == month).SumAsync(o => o.TotalAmount));
                totalYear.Add(await orders.Where(o => o.OrderDate!.Value.Month == month).SumAsync(o => o.TotalAmount));
            }

            return new List<IEnumerable<decimal>> { totalYear, thisYear, lastYear };

        }

        public async Task<int> GetMinYear()
        {
            return await _context.Orders.MinAsync(o => o.OrderDate!.Value.Year);
        }



        public async Task<IEnumerable<Order>> GetAllOrderAdmin()
        {
            return await _context.Orders.Include(x => x.Status).Include(z => z.OrderTrackings).Include(u=>u.User).ToListAsync();
        }
        public async Task<IEnumerable<string?>> GetShippingAddress(Guid userId)
        {
            return await _context.Orders.Where(p => p.UserId.Equals(userId)).Select(p => p.ShippingAddress).Distinct().ToListAsync();
        }

        public async Task<decimal> GetTotalAmount(Guid userId)
        {
            return await _context.Orders.Where(o => o.UserId.Equals(userId)).SumAsync(p => p.TotalAmount);
        }

        public async Task ApproveOrder(int orderId)
        {
            var order = await _context.Orders.Where(x=>x.Id == orderId).FirstOrDefaultAsync();
            var statusCheck = await _context.Statuses.Where(s=>s.Name.Equals("Processing")).FirstOrDefaultAsync();
            order.Status = statusCheck;
            await _context.SaveChangesAsync();
            
        }
    }
}
