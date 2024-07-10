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

		public IEnumerable<Review> getAllReviewByProduct(Guid productId)
		{
			return _unitOfWork.ReviewRepository.getAllReviewByProduct(productId);
		}

		public float getAverageRating(Guid productId)
		{
			var reviewList = getAllReviewByProduct(productId);
			float averageRating = 0;
			foreach (var review in reviewList)
			{
				averageRating += review.Rating;
			}
			return averageRating / reviewList.Count();
		}

		public int getTotalRating(Guid productId)
		{
			return getAllReviewByProduct(productId).Count();
		}

		public void AddReview(Review review)
		{
			_unitOfWork.ReviewRepository.AddReview(review);
		}

		public void DeleteReview(int reviewId)
		{
			_unitOfWork.ReviewRepository.DeleteReview(reviewId);
		}
	}
}
