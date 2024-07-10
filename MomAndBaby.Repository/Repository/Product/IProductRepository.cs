using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IProductRepository
    {
        Task<Product?> GetById(Guid productId);
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetNewItems();
        //Task<Product> UpdateTotalStar(Guid ProductId, int newRating);
        Task<IEnumerable<Product>> GetHighestRating();
        Task<IEnumerable<Product>> GetTrendingItems();
        //Task<Product> UpdatePurchase(Guid ProductId);
        Task CreateProduct(Product product);
        void UpdateProduct(Product product);
        Task DeleteProduct(Guid productIds);
        Task<bool> NameExistAsync(string name);
        Task<bool> NameUpdateExistAsync(Guid productId, string name);
        Task<List<Product>> GetProductsByIdsAsync(List<Guid> productIds);

    }
}
