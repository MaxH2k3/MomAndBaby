using System;
using System.Collections.Generic;

namespace MomAndBaby.BusinessObject.Entity
{
    public partial class Gift
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public int? ExchangePoint { get; set; }
        public Guid? ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
