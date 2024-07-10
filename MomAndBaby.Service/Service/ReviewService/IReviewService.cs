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
		public IEnumerable<Review> getAllReviewByProduct(Guid productId);
		public float getAverageRating(Guid productId);
		public int getTotalRating(Guid productId);
		public void AddReview(Review review);
		public void DeleteReview(int reviewId);
	}
}
