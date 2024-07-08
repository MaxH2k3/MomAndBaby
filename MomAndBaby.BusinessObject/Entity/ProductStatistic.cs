using System;
using System.Collections.Generic;

namespace MomAndBaby.BusinessObject.Entity
{
    public partial class ProductStatistic
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int TotalPurchase { get; set; }
        public int TotalReview { get; set; }
        public int AverageStar { get; set; }

        public virtual Product Product { get; set; }
    }
}
