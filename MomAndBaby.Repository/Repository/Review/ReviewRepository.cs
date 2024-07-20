using Microsoft.EntityFrameworkCore;
using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MomAndBabyContext _context;

        public ReviewRepository(MomAndBabyContext context)
        {
            _context = context;
        }

		public async Task<IEnumerable<Review>> GetAllReviewByProduct(Guid productId)
		{
			return await _context.Reviews.Where(r => r.ProductId.Equals(productId) && r.Status == true).ToListAsync();
		}
		public async Task AddReview(Review review)
		{
			await _context.Reviews.AddAsync(review);
		}

		public async Task DeleteReview(int reviewId)
		{
			var reviewToDelete = await _context.Reviews.FirstOrDefaultAsync(r => r.Id.Equals(reviewId));
			if (reviewToDelete != null)
			{
				_context.Reviews.Remove(reviewToDelete);
			}
		}

		public async Task EditReview(Review review, int reviewId)
		{
			var reviewToEdit = await _context.Reviews.FirstOrDefaultAsync(r => r.Id.Equals(reviewId));
			if (reviewToEdit != null)
			{
				reviewToEdit.CreatedAt = DateTime.Now;
				reviewToEdit.Comment = review.Comment;
				reviewToEdit.Rating = review.Rating;
			}
		}

		public async Task<Review> GetReviewById(int id)
		{
			return await _context.Reviews.FirstOrDefaultAsync(r => r.Id.Equals(id));
		}
	}
}
