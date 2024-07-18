using MomAndBaby.BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service.Helper
{
    public class EnumHelper
    {
        public static NotificationType GetNotificationType(string type)
        {
            try
            {
                return (NotificationType)Enum.Parse(typeof(NotificationType), type);
            } catch
            {
                return NotificationType.Unknown;
            }
        } 
    }
}
