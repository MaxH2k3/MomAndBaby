using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Repository.Uow;

namespace MomAndBaby.Service
{
    public class ArticleService : IArticleService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ArticleService()
		{
			_unitOfWork = new UnitOfWork();
		}

		public ArticleService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public PaginatedList<ArticleDTO> GetListArticleDTO(int pageNumber, int pageSize)
		{
			var articles = _unitOfWork.ArticleRepository.GetListArticle(pageNumber, pageSize);
			var totalArticles = _unitOfWork.ArticleRepository.GetTotalArticlesCount();

			var articleDTOs = articles.Select(article => new ArticleDTO
			{
				Id = article.Id,
				Title = article.Title,
				Content = article.Content,
				AuthorName = article.Author?.FullName,
				CreatedAt = article.CreatedAt
			}).ToList();

			return new PaginatedList<ArticleDTO>(articleDTOs, totalArticles, pageNumber, pageSize);
		}


        public PaginatedList<Article> GetListArticle(int pageNumber, int pageSize)
        {
            var articles = _unitOfWork.ArticleRepository.GetListArticle(pageNumber, pageSize);
            var totalArticles = _unitOfWork.ArticleRepository.GetTotalArticlesCount();

            return new PaginatedList<Article>(articles, totalArticles, pageNumber, pageSize);
        }

        public ArticleDTO GetArticleDTOById(int id)
        {
			var article = _unitOfWork.ArticleRepository.GetArticleById(id);

			var articleDTO = new ArticleDTO
			{
				Id = article.Id,
				Title = article.Title,
				Content = article.Content,
				AuthorName = article.Author?.FullName,
				CreatedAt = article.CreatedAt
			};

			return articleDTO;
        }


        public void AddArticle(Article article)
		{
			_unitOfWork.ArticleRepository.AddArticle(article);
		}

		public void UpdateArticle(Article article, int articleId)
		{
			_unitOfWork.ArticleRepository.UpdateArticle(article, articleId);
		}

		public void DeleteArticle(int id)
		{
			_unitOfWork.ArticleRepository.DeleteArticle(id);
		}

        public Article GetArticleById(int id)
        {
			return _unitOfWork.ArticleRepository.GetArticleById(id);
		}
    }
}
