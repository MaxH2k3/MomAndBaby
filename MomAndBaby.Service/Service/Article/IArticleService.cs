using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;

namespace MomAndBaby.Service
{
    public interface IArticleService
    {
		public PaginatedList<ArticleDTO> GetListArticle(int pageNumber, int pageSize);
        public ArticleDTO GetArticleDTOById(int id);
		public Article GetArticleById(int id); 
		public void AddArticle(Article article);
		public void UpdateArticle(Article article, int articleId);
		public void DeleteArticle(int id);
	}
}
