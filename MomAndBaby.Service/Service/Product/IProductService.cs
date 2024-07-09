using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.ProductDto;

namespace MomAndBaby.Service
{
    public interface IProductService
    {
        Task<ProductDto> GetById(Guid productId);
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetNewItems();
        //Task<Product> UpdateTotalStar(Guid ProductId, int newRating);
        Task<IEnumerable<Product>> GetHighestRating();
        Task<IEnumerable<Product>> GetTrendingItems();

        //Task<IEnumerable<Product>> GetTrendingItems();
        Task<bool> CreateProduct(ProductDto dto);
        Task<bool> UpdateProduct(ProductDto dto);
        Task<bool> SoftDeleteProduct(Guid productId);
        Task<IEnumerable<ProductDto>> GetRelatedProducts(int categoryId);
        IEnumerable<Product> GetFilteredProducts(decimal? startPrice, decimal? endPrice, int? numOfStars, string? sortCriteria);

    }

}
