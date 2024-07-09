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
    }
}