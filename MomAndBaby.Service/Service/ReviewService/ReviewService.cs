using MomAndBaby.BusinessObject.Entity;
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

		public async Task<int> GetTotalRating(Guid productId)
		{
			return (await GetAllReviewByProduct(productId)).Count();
		}
			
		public async Task AddReview(Review review)
		{
			await _unitOfWork.ReviewRepository.AddReview(review);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task DeleteReview(int reviewId)
		{
			await _unitOfWork.ReviewRepository.DeleteReview(reviewId);
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
