﻿using System;
using System.Collections.Generic;

namespace MomAndBaby.Entity
{
    public partial class Review
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProductId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Status { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
