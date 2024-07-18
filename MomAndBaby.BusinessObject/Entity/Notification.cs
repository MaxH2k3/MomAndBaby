using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Entity
{
    public class Notification
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string TypeMessage { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual User User { get; set; } = null!;
    }
}
