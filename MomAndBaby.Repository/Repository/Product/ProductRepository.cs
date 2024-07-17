using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;

namespace MomAndBaby.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MomAndBabyContext _context;

        public ProductRepository(MomAndBabyContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetById(Guid productId)
        {
            return await _context.Products.Include(x => x.CategoryNavigation).FirstOrDefaultAsync(x => x.Id == productId);
        }
        
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                .Where(x => x.Status.Equals(StatusConstraint.AVAILABLE))
                .Include(p => p.CategoryNavigation)
                .Include(p=>p.Statistic)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllShopping()
        {
            return await _context.Products
                .Where(x => !x.Status.Equals(StatusConstraint.DELETE))
                .Include(p => p.CategoryNavigation)
                .Include(p=>p.Statistic)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetHighestRating()
        {
            return await _context.Products.Include(p => p.Statistic).OrderByDescending(p => p.Statistic.AverageStar).Take(4).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetNewItems()
        {
            return await _context.Products.Where(p => p.CreatedAt.HasValue).OrderByDescending(p => p.CreatedAt).Take(4).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetTrendingItems()
        {
            return await _context.Products.Include(p => p.Statistic).OrderByDescending(p => p.Statistic.TotalPurchase).Take(8).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetRelatedProducts(int categoryId)
        {
            return await _context.Products.Include(x => x.Statistic).Where(x => x.CategoryId == categoryId).Take(6).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task DeleteProduct(Guid productId)
        {
            await _context.Products.Where(x => x.Id == productId)
                .ForEachAsync(x => x.Status = "Deleted");
        }

        public async Task<bool> NameExistAsync(string name)
        {
            return await _context.Products.AnyAsync(x => x.Name == name);
        }
        
        public async Task<bool> NameUpdateExistAsync(Guid productId, string name)
        {
            return await _context.Products.AnyAsync(x => x.Name == name && !x.Id.Equals(productId));
        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<Guid> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _context.Products.Include(p => p.Statistic).ToList();
        }

        public async Task<Tuple<List<string>, List<int>>> GetStatisticsProductCategory()
        {
            var result = await _context.Products
                .Include(p => p.CategoryNavigation)
                .GroupBy(p => p.CategoryNavigation!.Name)
                .Select(g => new { CategoryName = g.Key, Count = g.Count() })
                .ToListAsync();

            var categoryNames = result.Select(x => x.CategoryName!.ToString()).ToList();
            var counts = result.Select(x => x.Count).ToList();

            return Tuple.Create(categoryNames, counts);

        }
        

        //public async Task<IEnumerable<Product>> GetTrendingItems()
        //{
        //    return await _context.Products.Where(p => p.TotalPurchase.HasValue).OrderByDescending(p => p.TotalPurchase).ToListAsync();
        //}

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
