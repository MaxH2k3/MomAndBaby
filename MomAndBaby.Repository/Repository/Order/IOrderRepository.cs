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
        Task<IEnumerable<string?>> GetShippingAddress(Guid userId);
        Task<decimal> GetTotalAmount(Guid userId);
        Task<int> CreateOrder(Order order);
        // Task<>
    }
}
