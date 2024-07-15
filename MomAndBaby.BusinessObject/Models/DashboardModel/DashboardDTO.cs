using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Models
{
    public class DashboardDTO
    {
        public IEnumerable<Voucher> Vouchers { get; set; } = new List<Voucher>();
        public IEnumerable<Gift> Gifts { get; set; } = new List<Gift>();
        public Tuple<List<string>, List<int>> StatisticCategory { get; set; } = new Tuple<List<string>, List<int>>(new List<string>(), new List<int>());
    }
}
