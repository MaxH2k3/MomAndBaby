using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Service.OrderService
{
    public interface IOrderService{
        Task<IEnumerable<OrderResponseModel>> GetAllOrder();
        Task<Order> GetOrderById(int orderId);
        Task<bool> UpdateOrderAddress(string newAddress, int orderId);
        Task<IEnumerable<OrderDetailResponseModel>> GetAllOrderDetailOrder(int orderId);
        Task<int> CreateOrder(Order order); 
        Task CreateOrderDetail(List<OrderDetail> orderDetail);
        Task<bool> CompleteOrder(OrderTracking orderTracking);
        Task<OrderTracking> GetOrderTracking(int orderId);

    }
}