using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Service.OrderService
{
        public interface IOrderService{
                Task<IEnumerable<OrderResponseModel>> GetAllOrder(Guid userId);
                Task<OrderResponseModel> GetOrderById(int orderId);
                Task<bool> UpdateOrderAddress(string newAddress, int orderId);
                Task<IEnumerable<OrderDetailResponseModel>> GetAllOrderDetailOrder(int orderId);
                Task<int> CreateOrder(Order order); 
                Task<bool> CreateOrderDetail(List<OrderDetail> orderDetail);
                Task<bool> CompleteOrder(OrderTracking orderTracking);
                Task<OrderTracking> GetOrderTracking(int orderId);
                Task<IEnumerable<OrderResponseModel>> GetAllOrderAdmin();
                Task<IEnumerable<IEnumerable<decimal>>> GetTotalAmount();
                Task<IEnumerable<decimal>> GetTotalAmount(int year);
                Task<IEnumerable<string?>> GetShippingAddress(Guid userId);
                Task<decimal> GetTotalAmount(Guid userId);
                Task ApproveOrder(int orderId);
                Task<int> GetTotalOrder();
        Task<bool> ApprovalTracking(int orderId);
        Task<bool> CancelOrder(int orderId, bool isCheck);
        Task<int?> GetStatusOrder(int id);
    }
}