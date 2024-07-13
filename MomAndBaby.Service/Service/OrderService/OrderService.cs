using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Repository;
using MomAndBaby.Repository.Uow;

namespace MomAndBaby.Service.OrderService
{
    public class OrderService : IOrderService{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;     
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetAllOrder()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrder();
            return _mapper.Map<IEnumerable<OrderResponseModel>>(orders);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _unitOfWork.OrderRepository.GetOrderById(orderId);
        }

        public async Task<bool> UpdateOrderAddress(string newAddress, int orderId)
        {
            var result = _unitOfWork.OrderRepository.UpdateAddress(newAddress, orderId);
            if (result.IsFaulted)
            {
                return false;
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDetailResponseModel>> GetAllOrderDetailOrder(int orderId)
        {
            var orderDetails= await _unitOfWork.OrderRepository.GetAllOrderDetailOrder(orderId);
            return _mapper.Map<IEnumerable<OrderDetailResponseModel>>(orderDetails);
        }

        public async Task<int> CreateOrder(Order order)
        {
            return await _unitOfWork.OrderRepository.CreateOrder(order);
        }

        public async Task CreateOrderDetail(List<OrderDetail> orderDetail)
        {
            await _unitOfWork.OrderDetailRepository.CreateOrderDetail(orderDetail);
        }

        public async Task<bool> CompleteOrder(OrderTracking orderTracking)
        {
            var result = _unitOfWork.OrderTrackingRepository.CreateOrderTracking(orderTracking);
            if(result.IsFaulted){
                return false;
            }
            return true;
        }

        public async Task<OrderTracking> GetOrderTracking(int orderId)
        {
            return await _unitOfWork.OrderTrackingRepository.GetOrderTrackingAsync(orderId);
        }
    }
}