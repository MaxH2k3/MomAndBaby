using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Service
{
    public interface IArticleService
    {
		public Task<PaginatedList<ArticleDTO>> GetListArticleDTO(int pageNumber, int pageSize);
        public Task<PaginatedList<Article>> GetListArticle(int pageNumber, int pageSize, string searchTerm = "");
		public Task<PaginatedList<Article>> GetListActiveArticle(int pageNumber, int pageSize, string searchTerm = "");
		public Task<Article> GetNewestArticle();
		public Task<ArticleDTO> GetArticleDTOById(int id);
		public Task<Article?> GetArticleById(int id); 
		public Task AddArticle(Article article);
		public Task UpdateArticle(Article article, int articleId);
		public Task SoftDeleteArticle(int id);
		public Task HardDeleteArticle(int id);
		public Task RestoreArticle(int id);
        Task<int> GetTotalArticle();
    }
}
