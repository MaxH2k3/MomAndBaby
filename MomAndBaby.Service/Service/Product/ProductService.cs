using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Repository.Uow;

namespace MomAndBaby.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ProductDto> GetById(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetById(productId);
            var mapper = _mapper.Map<ProductDto>(product);
            return mapper;
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

            if (dto.ImageFile is null || dto.ImageFile.Length < 0)
            {
                throw new ArgumentException("Image is required.");
            }
            
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.ImageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ImageFile.CopyToAsync(fileStream);
            }
            
            dto.Image = "/images/uploads/" + uniqueFileName;
            
            var check = await _unitOfWork.ProductRepository.NameExistAsync(dto.Name);
            if (check)
            {
                throw new ArgumentException("Product name already exists.");
            }
            
            var mapper = _mapper.Map<Product>(dto);
            mapper.Id = Guid.NewGuid();
            await _unitOfWork.ProductRepository.CreateProduct(mapper);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateProduct(ProductDto dto)
        {
            var productId = (Guid)dto.Id;
            var product = await _unitOfWork.ProductRepository.GetById(productId);

            if (product is null)
            {
                throw new ArgumentException("Product not exists.");
            }
            
            var check = await _unitOfWork.ProductRepository.NameUpdateExistAsync(productId, dto.Name);
            if (check)
            {
                throw new ArgumentException("Product name already exists.");
            }
            
            if (dto.ImageFile is not null && dto.ImageFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(dto.Image))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, dto.Image.TrimStart('/'));
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(fileStream);
                }
                dto.Image = "/images/uploads/" + uniqueFileName;
            }

            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.Image = dto.Image;
            product.Description = dto.Description;
            product.UnitPrice = (decimal)dto.UnitPrice;
            product.Stock = (int)dto.Stock;
            product.Status = dto.Status;
            product.UpdatedAt = DateTime.Now;

            _unitOfWork.ProductRepository.UpdateProduct(product);
            return await _unitOfWork.SaveChangesAsync();

        }

        public async Task<bool> SoftDeleteProduct(Guid productId)
        {
            await _unitOfWork.ProductRepository.DeleteProduct(productId);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetRelatedProducts(int categoryId)
        {
            var products = await _unitOfWork.ProductRepository.GetRelatedProducts(categoryId);
            var mapper = _mapper.Map<IEnumerable<ProductDto>>(products);
            return mapper;
        }

        public IEnumerable<Product> GetFilteredProducts(decimal? startPrice, decimal? endPrice, int? numOfStars, string sortCriteria)
        {
            var productsQuery =  _unitOfWork.ProductRepository.GetAllProduct();
            if (startPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.UnitPrice >= startPrice);
            }
            if (endPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.UnitPrice <= endPrice);
            }

            if (numOfStars.HasValue)
            {
                if(numOfStars == 5)
                {
                    productsQuery = productsQuery.Where(p => p.Statistic.AverageStar == numOfStars);
                }
                else
                {
                    productsQuery = productsQuery.Where(p => p.Statistic.AverageStar >= numOfStars && p.Statistic.AverageStar < (numOfStars + 1));
                }
                
            }

            switch (sortCriteria)
            {
                case "Purchases":
                    productsQuery = productsQuery.OrderByDescending(p => p.Statistic.TotalPurchase);
                    break;
                case "Star":
                    productsQuery = productsQuery.OrderByDescending(p => p.Statistic.AverageStar);
                    break;
                case "Date":
                    productsQuery = productsQuery.OrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    break;
                
            }

            return productsQuery;

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
