using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.CartSessionModel;

namespace MomAndBaby.Repository
{
    public interface IProductRepository
    {
        Task<Product?> GetById(Guid productId);
        Task<IEnumerable<Product>> GetAllShopping();
        // Task<IEnumerable<Product>> GetAllAdmin();
        Task<Tuple<int, List<Product>>> SearchAdmin(int currentPage, string searchValue);
        IEnumerable<Product> GetAllProduct();
        Task<IEnumerable<Product>> GetNewItems();
        //Task<Product> UpdateTotalStar(Guid ProductId, int newRating);
        Task<IEnumerable<Product>> GetHighestRating();
        Task<IEnumerable<Product>> GetTrendingItems();
        Task<IEnumerable<Product>> GetRelatedProducts(int categoryId);
        Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);
        Task<IEnumerable<string?>> GetAllCompany();
        Task<IEnumerable<string?>> GetOriginals();
        Task<IEnumerable<Product>> GetListProductByCompany(string companyName);
        
        //Task<Product> UpdatePurchase(Guid ProductId);
        
        //CRUD
        Task CreateProduct(Product product);
        void UpdateProduct(Product product);
        Task DeleteProduct(Guid productIds);
        Task<bool> NameExistAsync(string name);
        Task<bool> NameUpdateExistAsync(Guid productId, string name);
        Task<List<Product>> GetProductsByIdsAsync(List<Guid> productIds);
        Task<Tuple<List<string>, List<int>>> GetStatisticsProductCategory();
        Task<Dictionary<Guid, int>> CheckStock(IEnumerable<CartSessionModel> CartSessionModels);
        void UpdateStock(List<OrderDetail> orderDetails);
        Task<int> GetTotalProducts();
    }
}
