using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Enums
{
    public enum OrderStatus
    {
        Processing = 1,
        Confirmed = 2,
        Packaged = 3,
        Delivered = 4,
        Completed = 5,
        Cancelled = 6
    }
}
