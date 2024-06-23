using System;
using System.Collections.Generic;

namespace MomAndBaby.BusinessObject.Entity
{
    public partial class Voucher
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public int? Amount { get; set; }

        public virtual User? CreatedByNavigation { get; set; }
    }
}
