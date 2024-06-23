using MomAndBaby.Entity;

namespace MomAndBaby.Models.ProductModel
{
    public class ProductStoreModel
    {
        public IEnumerable<Product> ListAllProducts { get; set; } = new List<Product>();
        public IEnumerable<Product> ListNewItems { get; set; } = new List<Product>();

        public IEnumerable<Product> ListHighestRatingItems { get; set; } = new List<Product>();
        public IEnumerable<Product> ListTrendingProduct { get; set; } = new List<Product>();
    }
}
