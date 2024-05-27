using System;
using System.Collections.Generic;

namespace MomAndBaby.Entity
{
    public partial class User
    {
        public User()
        {
            Articles = new HashSet<Article>();
            Messages = new HashSet<Message>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Vouchers = new HashSet<Voucher>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }
        public int? TotalPoints { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
