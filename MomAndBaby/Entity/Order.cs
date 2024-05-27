using System;
using System.Collections.Generic;

namespace MomAndBaby.Entity
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OrderTrackings = new HashSet<OrderTracking>();
        }

        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public int? StatusId { get; set; }
        public string? ShippingAddress { get; set; }
        public int? VoucherId { get; set; }

        public virtual Status? Status { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderTracking> OrderTrackings { get; set; }
    }
}
