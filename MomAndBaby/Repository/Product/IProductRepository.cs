using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable< Product>> GetAll();
    }
}
