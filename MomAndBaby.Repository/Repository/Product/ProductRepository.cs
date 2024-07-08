using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;

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
            return await _context.Products.Include(p=>p.Statistic).ToListAsync();
        }



        public async Task<IEnumerable<Product>> GetHighestRating()
        {
            return await _context.Products.Include(p => p.Statistic).OrderByDescending(p => p.Statistic.AverageStar).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetNewItems()
        {
            return await _context.Products.Where(p => p.CreatedAt.HasValue).OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetTrendingItems()
        {
            return await _context.Products.Include(p => p.Statistic).OrderByDescending(p => p.Statistic.TotalPurchase).ToListAsync();
        }

        //public async Task<Product> UpdateTotalStar(Guid productId, int newRating)
        //{
        //    var product = await _context.Products.FindAsync(productId);
        //    var review = await _context.Reviews.Where(r => r.ProductId == productId).ToListAsync();
        //    int sum = 0;
        //    foreach (var item in review)
        //    {
        //        sum += item.Rating;
        //    }
        //    if (product == null)
        //    {
        //        throw new Exception("Product not found");
        //    }

        //    //update the product's total reviews and total stars
        //    product.TotalReviewer += 1;
        //    sum += newRating;


        //    //Calculate the new average rating
        //    product.TotalStar = sum/product.TotalReviewer;

        //    await _context.SaveChangesAsync();
        //    return product;

        //}

        //public async Task<Product> UpdatePurchase(Guid ProductId)
        //{
        //    var product = await _context.Products.FindAsync(ProductId);

        //    product.TotalPurchase += 1;

        //    await _context.SaveChangesAsync();
        //    return product;
        //}


    }
}
