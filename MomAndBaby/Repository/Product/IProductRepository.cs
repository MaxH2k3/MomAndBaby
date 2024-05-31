using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetNewItems();

        Task<Product> UpdateTotalStar(Guid ProductId, int newRating);
        Task<IEnumerable<Product>> GetHighestRating();

        Task<IEnumerable<Product>> GetTrendingItems();

        Task<Product> UpdatePurchase(Guid ProductId);
    }
}
