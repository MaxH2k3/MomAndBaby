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

        public IEnumerable<Article> GetListArticle(int pageNumber, int pageSize)
		{
			return _context.Articles
						   .Include(article => article.Author)
						   .Skip((pageNumber - 1) * pageSize)
						   .Take(pageSize)
						   .ToList();
		}

		public int GetTotalArticlesCount()
		{
			return _context.Articles.Count();
		}

        public Article GetArticleById(int id)
        {
			return _context.Articles.Include(article => article.Author).FirstOrDefault(a => a.Id.Equals(id));
        }

		public void AddArticle(Article article)
		{
			_context.Articles.Add(article);
			_context.SaveChanges();
		}

		public void UpdateArticle(Article article, int articleId)
		{
			var articleToUpdate = GetArticleById(articleId);
			if (articleToUpdate != null)
			{
				articleToUpdate.Title = article.Title;
				articleToUpdate.Content = article.Content;
				_context.Update(articleToUpdate);
				_context.SaveChanges();
			}
		}

		public void DeleteArticle(int id)
		{
			var articleToDelete = GetArticleById(id);
			if (articleToDelete != null)
			{
				_context.Remove(articleToDelete);
				_context.SaveChanges();
			}
		}
	}
}
