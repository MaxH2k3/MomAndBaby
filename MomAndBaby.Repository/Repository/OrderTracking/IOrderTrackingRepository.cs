using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IOrderTrackingRepository
    {
        Task CreateOrderTracking(OrderTracking orderTracking);
        Task<bool> CancelOrder(int orderId);
        Task <OrderTracking> GetOrderTrackingAsync(int orderId);
        Task UpdateOrderTracking(OrderTracking orderTracking);
    }
}
