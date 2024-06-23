using System;
using System.Collections.Generic;

namespace MomAndBaby.BusinessObject.Entity
{
    public partial class Product
    {
        public Product()
        {
            Gifts = new HashSet<Gift>();
            OrderDetails = new HashSet<OrderDetail>();
            Reviews = new HashSet<Review>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Gift> Gifts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
