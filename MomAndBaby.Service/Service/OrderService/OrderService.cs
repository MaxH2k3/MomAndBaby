using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Repository;
using MomAndBaby.Repository.Uow;

namespace MomAndBaby.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetAllOrder(Guid userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrder(userId);
            return _mapper.Map<IEnumerable<OrderResponseModel>>(orders);
        }

        public async Task<OrderResponseModel> GetOrderById(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderById(orderId);
            return _mapper.Map<OrderResponseModel>(order);
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
            var orderDetails = await _unitOfWork.OrderRepository.GetAllOrderDetailOrder(orderId);
            return _mapper.Map<IEnumerable<OrderDetailResponseModel>>(orderDetails);
        }

        public async Task<int> CreateOrder(Order order)
        {
            return await _unitOfWork.OrderRepository.CreateOrder(order);
        }

        public async Task<bool> CreateOrderDetail(List<OrderDetail> orderDetail)
        {
            await _unitOfWork.OrderDetailRepository.CreateOrderDetail(orderDetail);

            _unitOfWork.ProductRepository.UpdateStock(orderDetail);

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> CompleteOrder(OrderTracking orderTracking)
        {
            var result = _unitOfWork.OrderTrackingRepository.CreateOrderTracking(orderTracking);
            if (result.IsFaulted)
            {
                return false;
            }
            return true;
        }

        public async Task<OrderTracking> GetOrderTracking(int orderId)
        {
            return await _unitOfWork.OrderTrackingRepository.GetOrderTrackingAsync(orderId);
        }

        public async Task<IEnumerable<IEnumerable<decimal>>> GetTotalAmount()
        {
            return await _unitOfWork.OrderRepository.GetTotalMoney();
        }

        public async Task<IEnumerable<decimal>> GetTotalAmount(int year)
        {
            return await _unitOfWork.OrderRepository.GetTotalMoneyByYear(year);
        }

        public async Task<IEnumerable<OrderResponseModel>> GetAllOrderAdmin()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrderAdmin();
            return _mapper.Map<IEnumerable<OrderResponseModel>>(orders);
        }
        public async Task<IEnumerable<string?>> GetShippingAddress(Guid userId)
        {
            return await _unitOfWork.OrderRepository.GetShippingAddress(userId);
        }

        public async Task<decimal> GetTotalAmount(Guid userId)
        {
            return await _unitOfWork.OrderRepository.GetTotalAmount(userId);
        }

        public async Task ApproveOrder(int orderId)
        {
             await _unitOfWork.OrderRepository.ApproveOrder(orderId);
        }
    }
}