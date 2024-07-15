using System.Collections;
using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrder(Guid userId);
        Task<Order> GetOrderById(int id);
        Task UpdateAddress(string newAddress, int orderId);
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailOrder(int orderId);

        Task<int> CreateOrder(Order order);
        // Task<>
    }
}
