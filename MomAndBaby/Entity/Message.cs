using System;
using System.Collections.Generic;

namespace MomAndBaby.Entity
{
    public partial class Message
    {
        public int Id { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? ReceiverId { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsSystem { get; set; }

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
