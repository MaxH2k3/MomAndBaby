using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Repository.Uow;

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

        public async Task<IEnumerable<Product>> GetHighestRating()
        {
            return await _unitOfWork.ProductRepository.GetHighestRating();
        }

        public async Task<IEnumerable<Product>> GetNewItems()
        {
            return await _unitOfWork.ProductRepository.GetNewItems();
        }

        public async Task<IEnumerable<Product>> GetTrendingItems()
        {
            return await _unitOfWork.ProductRepository.GetTrendingItems();
        }

        //public async Task<Product> UpdateTotalStar(Guid ProductId, int newRating)
        //{
        //    return await _unitOfWork.ProductRepository.UpdateTotalStar(ProductId, newRating);
        //}
    }
    
}
