using System;
using System.Collections.Generic;

namespace MomAndBaby.BusinessObject.Entity
{
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? Author { get; set; }
    }
}
