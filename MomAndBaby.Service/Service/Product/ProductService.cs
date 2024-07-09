using AutoMapper;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Repository.Uow;

namespace MomAndBaby.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        public async Task<bool> CreateProduct(ProductDto dto)
        {
            var check = await _unitOfWork.ProductRepository.NameExistAsync(dto.Name);
            if (check)
            {
                throw new ArgumentException("Product name already exists.");
            }
            
            var mapper = _mapper.Map<Product>(dto);
            mapper.Id = Guid.NewGuid();
            //mapper.Statistic = new ProductStatistic
            //{
            //    ProductId = mapper.Id,
            //    ProductName = mapper.Name
            //};
            await _unitOfWork.ProductRepository.CreateProduct(mapper);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateProduct(ProductDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteProduct(List<Guid> productIds)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Product>> GetTrendingItems()
        //{
        //    return await _unitOfWork.ProductRepository.GetTrendingItems();
        //}

        //public async Task<Product> UpdateTotalStar(Guid ProductId, int newRating)
        //{
        //    return await _unitOfWork.ProductRepository.UpdateTotalStar(ProductId, newRating);
        //}
    }
    
}
