using Microsoft.EntityFrameworkCore;
using MomAndBaby.Configuration.Uow;
using MomAndBaby.Entity;
using MomAndBaby.Repository;

namespace MomAndBaby.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _unitOfWork.ProductRepository.GetAll();
        }
    }
    
}
