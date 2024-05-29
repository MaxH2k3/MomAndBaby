using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Set<Product>().ToListAsync();
        }
    }
}
