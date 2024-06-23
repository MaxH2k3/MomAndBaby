namespace MomAndBaby.BusinessObject.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
