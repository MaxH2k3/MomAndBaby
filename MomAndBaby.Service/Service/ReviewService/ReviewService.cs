using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.ReviewDTO;
using MomAndBaby.Repository.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service
{
	public class ReviewService : IReviewService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ReviewService()
		{
			_unitOfWork = new UnitOfWork();
		}

		public ReviewService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<PaginatedList<ReviewDTO>> GetListReviewDTO(int pageNumber, int pageSize)
		{
			var reviews = await _unitOfWork.ReviewRepository.GetListReview(pageNumber, pageSize);
			var totalReviews = await _unitOfWork.ReviewRepository.GetTotalReviewsCount();

			List<ReviewDTO> reviewDTOs = new List<ReviewDTO>();

			foreach (var review in reviews)
			{
				var user = await _unitOfWork.UserRepository.getUserById(review.UserId);
				var product = await _unitOfWork.ProductRepository.GetById(review.ProductId);

				var reviewDTO = new ReviewDTO
				{
					Id = review.Id,
					UserId = review.UserId,
					UserFullName = user?.FullName,
					ProductId = review.ProductId,
					ProductName = product?.Name,
					Rating = review.Rating,
					Comment = review.Comment,
					CreatedAt = review.CreatedAt,
					Status = review.Status,
				};

				reviewDTOs.Add(reviewDTO);
			}

			return new PaginatedList<ReviewDTO>(reviewDTOs, totalReviews, pageNumber, pageSize);
		}
		public async Task<PaginatedList<Review>> GetListReview(int pageNumber, int pageSize)
		{
			var reviews = await _unitOfWork.ReviewRepository.GetListReview(pageNumber, pageSize);
			var totalReviews = await _unitOfWork.ReviewRepository.GetTotalReviewsCount();

			return new PaginatedList<Review>(reviews, totalReviews, pageNumber, pageSize);
		}

		public async Task<IEnumerable<Review>> GetAllReviewByProduct(Guid productId)
		{
			return await _unitOfWork.ReviewRepository.GetAllReviewByProduct(productId);
		}

		public async Task<float> GetAverageRating(Guid productId)
		{
			var reviewList = await GetAllReviewByProduct(productId);
			float averageRating = 0;
			foreach (var review in reviewList)
			{
				averageRating += review.Rating;
			}
			return averageRating / reviewList.Count();
		}

		public async Task<float> GetAverageRatingAllReview()
		{
			var reviewList = await _unitOfWork.ReviewRepository.GetAllReview();
			float averageRating = 0;
			foreach (var review in reviewList)
			{
				averageRating += review.Rating;
			}
			averageRating = (float)Math.Round((averageRating / reviewList.Count()), 2);
			return averageRating;
		}

		public async Task<int> GetListReviewByRating(int rating)
		{
			var reviewList =  await _unitOfWork.ReviewRepository.GetListReviewByRating(rating);
			return reviewList.Count();
		}

		public async Task<int> GetListReviewLastWeek()
		{
			var reviewList = await _unitOfWork.ReviewRepository.GetListReviewByWeek();
			return reviewList.Count();
		}

		public async Task<int> ReviewCount()
		{
			var reviewList = await _unitOfWork.ReviewRepository.GetAllReview();
			return reviewList.Count();
		}

		public async Task<int> GetTotalRating(Guid productId)
		{
			return (await GetAllReviewByProduct(productId)).Count();
		}

		public async Task AddReview(Review review)
		{
			await _unitOfWork.ReviewRepository.AddReview(review);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task SoftDeleteReview(int reviewId)
		{
			await _unitOfWork.ReviewRepository.SoftDeleteReview(reviewId);
			await _unitOfWork.SaveChangesAsync();
		}
		public async Task HardDeleteReview(int reviewId)
		{
			await _unitOfWork.ReviewRepository.HardDeleteReview(reviewId);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task RestoreReview(int reviewId)
		{
			await _unitOfWork.ReviewRepository.RestoreReview(reviewId);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<Review> GetReviewById(int id)
		{
			return await _unitOfWork.ReviewRepository.GetReviewById(id);

		}

		public async Task EditReview(Review review, int reviewId)
		{
			await _unitOfWork.ReviewRepository.EditReview(review, reviewId);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}
