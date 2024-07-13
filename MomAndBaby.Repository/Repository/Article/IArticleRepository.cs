using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IArticleRepository
    {
		public Task<IEnumerable<Article>> GetListArticle(int pageNumber, int pageSize);
		public Task<int> GetTotalArticlesCount();
		public Task<Article?> GetArticleById(int id);
		public Task AddArticle(Article article);
		public Task UpdateArticle(Article article, int articleId);
		public Task DeleteArticle(int id);
	}
}
