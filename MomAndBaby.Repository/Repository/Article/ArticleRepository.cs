using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
	public class ArticleRepository : IArticleRepository
	{
		private readonly MomAndBabyContext _context;

		public ArticleRepository(MomAndBabyContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Article>> GetListArticle(int pageNumber, int pageSize, string searchTerm = "")
		{
			return await _context.Articles
						   .Where(a => a.Title.Contains(searchTerm) || a.Author!.FullName!.Contains(searchTerm))
						   .Include(article => article.Author)
						   .Skip((pageNumber - 1) * pageSize)
						   .Take(pageSize)
						   .ToListAsync();
		}

		public async Task<IEnumerable<Article>> GetListActiveArticle(int pageNumber, int pageSize, string searchTerm = "")
		{
			return await _context.Articles
						   .Where(a => a.Title.Contains(searchTerm) || a.Author!.FullName!.Contains(searchTerm))
						   .Where(a => a.Status == true)
						   .Include(article => article.Author)
						   .Skip((pageNumber - 1) * pageSize)
						   .Take(pageSize)
						   .ToListAsync();
		}

		public async Task<int> GetTotalArticlesCount()
		{
			return await _context.Articles.CountAsync();
		}

		public async Task<Article?> GetArticleById(int id)
		{
			return await _context.Articles.Include(article => article.Author).FirstOrDefaultAsync(a => a.Id.Equals(id));
		}

		public async Task AddArticle(Article article)
		{
			await _context.Articles.AddAsync(article);
		}

		public async Task UpdateArticle(Article article, int articleId)
		{
			var articleToUpdate = await GetArticleById(articleId);
			if (articleToUpdate != null)
			{
				articleToUpdate.Title = article.Title;
				articleToUpdate.Content = article.Content;
				_context.Update(articleToUpdate);
			}
		}

		public async Task SoftDeleteArticle(int id)
		{
			var articleToDelete = await GetArticleById(id);
			if (articleToDelete != null)
			{
				articleToDelete.Status = false;
			}
		}

		public async Task HardDeleteArticle(int id)
		{
			var articleToDelete = await GetArticleById(id);
			if (articleToDelete != null)
			{
				_context.Remove(articleToDelete);
			}
		}

		public async Task RestoreArticle(int id)
		{
			var articleToDelete = await GetArticleById(id);
			if (articleToDelete != null)
			{
				articleToDelete.Status = true;
			}
		}
	}
}
