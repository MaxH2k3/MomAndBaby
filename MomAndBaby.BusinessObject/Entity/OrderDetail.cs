using System;
using System.Collections.Generic;

namespace MomAndBaby.BusinessObject.Entity
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
