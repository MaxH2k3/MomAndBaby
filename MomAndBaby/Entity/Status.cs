using System;
using System.Collections.Generic;

namespace MomAndBaby.Entity
{
    public partial class Status
    {
        public Status()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
