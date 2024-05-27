using MomAndBaby.Entity;

namespace MomAndBaby.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MomAndBabyContext _context;

        public ProductRepository(MomAndBabyContext context)
        {
            _context = context;
        }
    }
}
