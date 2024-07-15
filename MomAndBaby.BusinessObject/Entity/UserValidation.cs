using System;
using System.Collections.Generic;

namespace MomAndBaby.BusinessObject.Entity
{
    public partial class UserValidation
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string? Otp { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }

       
    }
}
