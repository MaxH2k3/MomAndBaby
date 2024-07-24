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
        Packaged = 2,
        Delivered = 3,
        Completed = 4,
        Cancelled = 5
    }
}
