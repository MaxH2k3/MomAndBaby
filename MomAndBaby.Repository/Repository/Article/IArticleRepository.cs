using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
	public interface IArticleRepository
	{
		public Task<IEnumerable<Article>> GetListArticle(int pageNumber, int pageSize, string searchTerm = "");
		public Task<IEnumerable<Article>> GetListActiveArticle(int pageNumber, int pageSize, string searchTerm = "");
		public Task<int> GetTotalArticlesCount();
		public Task<Article> GetNewestArticle();
		public Task<Article?> GetArticleById(int id);
		public Task AddArticle(Article article);
		public Task UpdateArticle(Article article, int articleId);
		public Task SoftDeleteArticle(int id);
		public Task HardDeleteArticle(int id);
		public Task RestoreArticle(int id);
		Task<int> GetTotalArticle();

    }
}
