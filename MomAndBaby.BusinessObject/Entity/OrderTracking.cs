using System;
using System.Collections.Generic;

namespace MomAndBaby.BusinessObject.Entity
{
    public partial class OrderTracking
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public DateTime? OrderConfirmation { get; set; }
        public DateTime? Package { get; set; }
        public DateTime? Delivery { get; set; }
        public DateTime? Complete { get; set; }

        public virtual Order? Order { get; set; }
    }
}
