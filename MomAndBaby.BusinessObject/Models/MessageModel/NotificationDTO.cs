using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Models.MessageModel
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        //public string Avatar { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public NotificationType TypeMessage { get; set; }
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}
