using System.ComponentModel.DataAnnotations;

namespace MomAndBaby.BusinessObject.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Title cannot be blank.")]
		public string Title { get; set; } = null!;
		[Required(ErrorMessage = "Content cannot be blank.")]
		public string? Content { get; set; }
        public Guid? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
