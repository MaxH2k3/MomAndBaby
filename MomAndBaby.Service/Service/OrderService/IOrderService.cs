using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Service.OrderService
{
    public interface IOrderService{
        Task<IEnumerable<OrderResponseModel>> GetAllOrder();
        Task<Order> GetOrderById(int orderId);

    }
}