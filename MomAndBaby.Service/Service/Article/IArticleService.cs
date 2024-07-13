using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Service
{
    public interface IArticleService
    {
		public Task<PaginatedList<ArticleDTO>> GetListArticleDTO(int pageNumber, int pageSize);
        public Task<PaginatedList<Article>> GetListArticle(int pageNumber, int pageSize);
        public Task<ArticleDTO> GetArticleDTOById(int id);
		public Task<Article?> GetArticleById(int id); 
		public Task AddArticle(Article article);
		public Task UpdateArticle(Article article, int articleId);
		public Task DeleteArticle(int id);
	}
}
