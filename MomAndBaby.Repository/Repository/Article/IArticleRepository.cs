using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IArticleRepository
    {
		public IEnumerable<Article> GetListArticle(int pageNumber, int pageSize);
		public int GetTotalArticlesCount();
		public Article GetArticleById(int id);
		public void AddArticle(Article article);
		public void UpdateArticle(Article article, int articleId);
		public void DeleteArticle(int id);
	}
}
