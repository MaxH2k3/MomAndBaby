using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.CartSessionModel;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Repository.Uow;
using Newtonsoft.Json.Linq;

namespace MomAndBaby.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task<ProductDto> GetById(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetById(productId);
            var mapper = _mapper.Map<ProductDto>(product);
            return mapper;
        }

        public async Task<Tuple<int, List<ProductDto>>> GetAllAdmin(int currentPage, string searchValue = "")
        {
            var products = await _unitOfWork.ProductRepository.SearchAdmin(currentPage, searchValue);

            var mapperList = _mapper.Map<List<ProductDto>>(products.Item2);
            
            return Tuple.Create(products.Item1, mapperList);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _unitOfWork.ProductRepository.GetAllShopping();
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
            
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/product_images");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.ImageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ImageFile.CopyToAsync(fileStream);
            }
            
            dto.Image = "/images/product_images/" + uniqueFileName;
            
            var mapper = _mapper.Map<Product>(dto);
            mapper.Id = Guid.NewGuid();
            mapper.CreatedAt = DateTime.Now;
            await _unitOfWork.ProductRepository.CreateProduct(mapper);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateProduct(ProductDto dto)
        {
            var productId = (Guid)dto.Id;
            var product = await _unitOfWork.ProductRepository.GetById(productId);
            
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
                
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/product_images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(fileStream);
                }
                dto.Image = "/images/product_images/" + uniqueFileName;
            }

            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.Image = dto.Image;
            product.Description = dto.Description;
            product.UnitPrice = dto.UnitPrice;
            product.PurchasePrice = dto.PurchasePrice;
            product.Stock = dto.Stock;
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

        public async Task<IEnumerable<Product>> GetFilteredProducts(int? categoryId, string? companyName, string? original, decimal? startPrice, decimal? endPrice, int? numOfStars, string sortCriteria)
        {
            var productsQuery =  await _unitOfWork.ProductRepository.GetAllShopping();
            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }
            if (companyName != null)
            {
                productsQuery = productsQuery.Where(p => p.Company.Equals(companyName));
            }
            if (original != null)
            {
                productsQuery = productsQuery.Where(p => p.Original.Equals(original));
            }
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
                    productsQuery = productsQuery.Where(p => p.Statistic.AverageStar >= numOfStars);
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
                case "IncreasePrice":
                    productsQuery = productsQuery.OrderBy(p => p.UnitPrice);
                    break;
                case "DecreasePrice":
                    productsQuery = productsQuery.OrderByDescending(p => p.UnitPrice);
                    break;
                default:
                    break;
                
            }

            return productsQuery;

        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<Guid> productIds)
        {
            return await _unitOfWork.ProductRepository.GetProductsByIdsAsync(productIds);
        }

        //public async Task<IEnumerable<Product>> GetTrendingItems()
        //{
        //    return await _unitOfWork.ProductRepository.GetTrendingItems();
        //}
        public async Task<IEnumerable<ProductCategoryDto>> GetCategoryShopping()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategory();
            var productCategory = _mapper.Map<IEnumerable< ProductCategoryDto>>(categories);
            foreach (var category in productCategory)
            {
                var listProductByCategory = await _unitOfWork.ProductRepository.GetProductsByCategoryId(category.Id);
                category.NumberOfProduct = listProductByCategory.Count();
            }

            return productCategory;
            
        }


        public async Task<Tuple<List<string>, List<int>>> GetStatisticsProductCategory()
        {
            return await _unitOfWork.ProductRepository.GetStatisticsProductCategory();
        }

        public async Task<IEnumerable<ProductOriginalDto>> GetOriginalShopping()
        {
            
            var listOriginal = await _unitOfWork.ProductRepository.GetOriginals();
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(_configuration["Country:Flag"]);
                var data = JObject.Parse(response)["data"];

                // Tạo từ điển để dễ dàng tra cứu hình ảnh cờ
                var imageFlags = new Dictionary<string, string>();
                foreach (var country in data)
                {
                    var name = country["name"].ToString();
                    var imageFlag = country["flag"].ToString();
                    imageFlags[name] = imageFlag;
                }

                // Tạo danh sách kết quả ProductOriginalDto
                var result = new List<ProductOriginalDto>();
                foreach (var companyName in listOriginal)
                {
                    if (imageFlags.TryGetValue(companyName, out var imageFlag))
                    {
                        result.Add(new ProductOriginalDto
                        {
                            Name = companyName,
                            Image = imageFlag
                        });
                    }
                    else
                    {
                        result.Add(new ProductOriginalDto
                        {
                            Name = companyName,
                            Image = null // Hoặc gán giá trị mặc định nếu không tìm thấy cờ
                        });
                    }
                }

                return result;
            }

        }

        public async Task<IEnumerable<ProductCompanyDto>> GetCompanyShopping()
        {
            var companies = await _unitOfWork.ProductRepository.GetAllCompany();
            // Initialize a list of ProductCompanyDto
            var productCompanies = companies.Select(c => new ProductCompanyDto { Name = c }).ToList();
            foreach ( var company in productCompanies)
            {
                var listProductByCompany = await _unitOfWork.ProductRepository.GetListProductByCompany(company.Name);
                company.NumberOfProduct = listProductByCompany.Count();
            }
            return productCompanies;
        }

        public async Task<Dictionary<Guid, int>> CheckStock(IEnumerable<CartSessionModel> CartSessionModels)
        {
            return await _unitOfWork.ProductRepository.CheckStock(CartSessionModels);
        }

        /*public void UpdateStock(List<CartSessionModel> cartData)
        {
            _unitOfWork.ProductRepository.UpdateStock(cartData);
        }*/

        public async Task<int> GetTotalProduct()
        {
            return await _unitOfWork.ProductRepository.GetTotalProducts();
        }
    }
    
}
