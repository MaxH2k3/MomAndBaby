using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetNewItems();
<<<<<<< HEAD
<<<<<<< HEAD
        
=======
>>>>>>> parent of 0ef4ac0 (Merge branch 'Khang')
=======
>>>>>>> parent of 0ef4ac0 (Merge branch 'Khang')

        //Task<Product> UpdateTotalStar(Guid ProductId, int newRating);
        Task<IEnumerable<Product>> GetHighestRating();

        Task<IEnumerable<Product>> GetTrendingItems();

        //Task<Product> UpdatePurchase(Guid ProductId);

        Task CreateProduct(Product product);
        void UpdateProduct(Product product);
        Task DeleteProduct(List<Guid> productIds);
        Task<bool> NameExistAsync(string name);
    }
}
