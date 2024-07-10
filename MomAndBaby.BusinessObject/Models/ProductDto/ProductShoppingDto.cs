using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Models.ProductDto
{
    public class ProductShoppingDto
    {
        public string? Products { get; set; }
        public int TotalProductsCount { get; set; }
        public int FilteredProductsCount { get; set; }

        public ProductShoppingDto(string? products, int totalProductsCount, int filteredProductsCount)
        {
            Products = products;
            TotalProductsCount = totalProductsCount;
            FilteredProductsCount = filteredProductsCount;
        }

        public ProductShoppingDto()
        {
        }
    }
}
