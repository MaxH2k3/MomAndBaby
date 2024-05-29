using MomAndBaby.Entity;

namespace MomAndBaby.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
    }
}
