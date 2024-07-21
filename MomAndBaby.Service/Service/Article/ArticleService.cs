using Microsoft.EntityFrameworkCore;
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

        public async Task<PaginatedList<ArticleDTO>> GetListArticleDTO(int pageNumber, int pageSize)
		{
			var articles = await _unitOfWork.ArticleRepository.GetListArticle(pageNumber, pageSize);
			var totalArticles = await _unitOfWork.ArticleRepository.GetTotalArticlesCount();

			var articleDTOs = articles.Select(article => new ArticleDTO
			{
				Id = article.Id,
				Title = article.Title,
				Content = article.Content,
				AuthorName = article.Author?.FullName,
				AuthorId = article.AuthorId,
				CreatedAt = article.CreatedAt
			}).ToList();

			return new PaginatedList<ArticleDTO>(articleDTOs, totalArticles, pageNumber, pageSize);
		}

		public async Task<PaginatedList<Article>> GetListActiveArticle(int pageNumber, int pageSize, string searchTerm = "")
		{
			var articles = await _unitOfWork.ArticleRepository.GetListActiveArticle(pageNumber, pageSize, searchTerm);
			var totalArticles = await _unitOfWork.ArticleRepository.GetTotalArticlesCount();

			return new PaginatedList<Article>(articles, totalArticles, pageNumber, pageSize);
		}


		public async Task<PaginatedList<Article>> GetListArticle(int pageNumber, int pageSize, string searchTerm = "")
        {
            var articles = await _unitOfWork.ArticleRepository.GetListArticle(pageNumber, pageSize, searchTerm);
            var totalArticles = await _unitOfWork.ArticleRepository.GetTotalArticlesCount();

            return new PaginatedList<Article>(articles, totalArticles, pageNumber, pageSize);
        }

        public async Task<ArticleDTO> GetArticleDTOById(int id)
        {
			var article = await _unitOfWork.ArticleRepository.GetArticleById(id);

			var articleDTO = new ArticleDTO
			{
                Id = article!.Id,
                Title = article.Title,
				Content = article.Content,
				AuthorId = article.AuthorId,
				AuthorName = article.Author?.FullName,
				CreatedAt = article.CreatedAt
			};

			return articleDTO;
        }
		public async Task<Article> GetNewestArticle()
		{
			return await _unitOfWork.ArticleRepository.GetNewestArticle();
		}
		public async Task AddArticle(Article article)
		{
			await _unitOfWork.ArticleRepository.AddArticle(article);
            await _unitOfWork.SaveChangesAsync();
        }

		public async Task UpdateArticle(Article article, int articleId)
		{
			await _unitOfWork.ArticleRepository.UpdateArticle(article, articleId);
            await _unitOfWork.SaveChangesAsync();
        }

		public async Task SoftDeleteArticle(int id)
		{
			await _unitOfWork.ArticleRepository.SoftDeleteArticle(id);
            await _unitOfWork.SaveChangesAsync();
        }

		public async Task HardDeleteArticle(int id)
		{
			await _unitOfWork.ArticleRepository.HardDeleteArticle(id);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<Article?> GetArticleById(int id)
        {
			return await _unitOfWork.ArticleRepository.GetArticleById(id);
		}

		public async Task RestoreArticle(int id)
		{
			await _unitOfWork.ArticleRepository.RestoreArticle(id);
			await _unitOfWork.SaveChangesAsync();
		}

        public async Task<int> GetTotalArticle()
        {
            return await _unitOfWork.ArticleRepository.GetTotalArticle();
        }
    }
}
