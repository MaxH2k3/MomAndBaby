using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.CartSessionModel;
using System.Security.Cryptography.X509Certificates;

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
            return await _context.Products.Include(x => x.CategoryNavigation).AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetAllShopping()
        {
            return await _context.Products
                .Where(x => x.Status.Equals(StatusConstraint.AVAILABLE))
                .Include(p => p.CategoryNavigation)
                .Include(p => p.Statistic)
                .ToListAsync();
        }

        // public async Task<IEnumerable<Product>> GetAllAdmin()
        // {
        //     var products = _context.Products.AsQueryable();
        //     products = products.Where()
        //     
        //     return await _context.Products
        //         .Where(p => !p.Status.Equals(StatusConstraint.DELETE))
        //         .Include(p => p.CategoryNavigation)
        //         .Include(p=>p.Statistic)
        //         .ToListAsync();
        // }

        public async Task<Tuple<int, List<Product>>> SearchAdmin(int currentPage, string searchValue)
        {
            var products = _context.Products
                .Include(p => p.CategoryNavigation)
                .Include(p => p.Statistic)
                .AsQueryable();

            products = products.Where(x => !x.Status.Equals(StatusConstraint.DELETE));

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchValue.ToLower()));
            }

            var count = await products.CountAsync();

            products = products.OrderBy(x => x.Name).ThenBy(x => x.CreatedAt);
            
            var result = await products.Skip((currentPage - 1) * 8).Take(8).ToListAsync();

            return Tuple.Create(count, result);
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

        public async Task<IEnumerable<string?>> GetAllCompany()
        {
            return await _context.Products
                             .Select(p => p.Company)
                             .Distinct()
                             .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetListProductByCompany(string companyName)
        {
            return await _context.Products.Where(p => p.Company.Equals(companyName)).ToListAsync();
        }

        public async Task<IEnumerable<string?>> GetOriginals()
        {
            return await _context.Products.Where(p => p.Status.Equals(StatusConstraint.AVAILABLE))
                              .Select(p => p.Original)
                              .Distinct()
                              .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            return await _context.Products.Include(x => x.Statistic).Where(x => x.CategoryId == categoryId && x.Status.Equals(StatusConstraint.AVAILABLE)).ToListAsync();
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

        public async Task<Dictionary<Guid, int>> CheckStock(IEnumerable<CartSessionModel> CartSessionModels)
        {
            var errors = new Dictionary<Guid, int>();

            foreach (var cart in CartSessionModels)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(cart.Id));

                if (product != null)
                {
                    var existStock = product.Stock - cart.NumberOfProduct;

                    if (existStock < 0)
                    {
                        errors.Add(product.Id, product.Stock);
                    }

                }

            }

            return errors;
        }

        public void UpdateStock(List<OrderDetail> orderDetails)
        {
            foreach (var cartItem in orderDetails)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id.Equals(cartItem.ProductId));
                if (product == null)
                {
                    return;
                }
                product.Stock -= cartItem.Quantity;
                if (product.Stock == 0)
                {
                    product.Status = StatusConstraint.OUTOFSTOCK;
                }
                _context.Products.Update(product);
            };
            _context.SaveChanges();
        }

        public async Task<int> GetTotalProducts()
        {
            return await _context.Products.CountAsync();
        }

    }
}
