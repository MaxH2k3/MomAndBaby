using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IOrderDetailRepository
    {
        Task CreateOrderDetail(List<OrderDetail> orderDetail);
    }
}
