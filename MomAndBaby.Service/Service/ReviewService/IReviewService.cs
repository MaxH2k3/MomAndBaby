using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.ReviewDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.Service
{
	public interface IReviewService
	{
		public Task<int> ReviewCount();
		public Task<PaginatedList<ReviewDTO>> GetListReviewDTO(int pageNumber, int pageSize);
		public Task<PaginatedList<Review>> GetListReview(int pageNumber, int pageSize);
		public Task<IEnumerable<Review>> GetAllReviewByProduct(Guid productId);
		public Task<int> GetListReviewByRating(int rating);
		public Task<int> GetListReviewLastWeek();
		public Task<Review> GetReviewById(int id);
		public Task<float> GetAverageRating(Guid productId);
		public Task<float> GetAverageRatingAllReview();
		public Task<int> GetTotalRating(Guid productId);
		public Task AddReview(Review review);
		public Task EditReview(Review review, int reviewId);
		public Task SoftDeleteReview(int reviewId);
		public Task HardDeleteReview(int reviewId);
		public Task RestoreReview(int reviewId);
	}
}
