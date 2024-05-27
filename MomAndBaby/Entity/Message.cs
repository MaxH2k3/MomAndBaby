using System;
using System.Collections.Generic;

namespace MomAndBaby.Entity
{
    public partial class Message
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
