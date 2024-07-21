using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Models.ProductDto
{
    public class ProductPreview
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public string? Original { get; set; }
        public string? Company { get; set; }
        public string? Status { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int TotalPurchase { get; set; }
        public int TotalReview { get; set; }
        public double AverageStar { get; set; }
    }
}
