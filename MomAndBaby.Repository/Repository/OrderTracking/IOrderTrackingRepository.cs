using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IOrderTrackingRepository
    {
        Task CreateOrderTracking(OrderTracking orderTracking);
        Task <OrderTracking> GetOrderTrackingAsync(int orderId);
    }
}
