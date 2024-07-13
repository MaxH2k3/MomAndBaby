using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service
{
	public interface IReviewService
	{
		public Task<IEnumerable<Review>> GetAllReviewByProduct(Guid productId);
		public Task<Review> GetReviewById(int id);
		public Task<float> GetAverageRating(Guid productId);
		public Task<int> GetTotalRating(Guid productId);
		public Task AddReview(Review review);
		public Task EditReview(Review review, int reviewId);
		public Task DeleteReview(int reviewId);
	}
}
