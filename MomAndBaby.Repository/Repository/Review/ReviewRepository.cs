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
		public async Task<IEnumerable<Review>> GetAllReview()
		{
			return await _context.Reviews.ToListAsync();
		}
		public async Task<IEnumerable<Review>> GetListReview(int pageNumber, int pageSize)
		{
			return await _context.Reviews
						   .Skip((pageNumber - 1) * pageSize)
						   .Take(pageSize)
						   .ToListAsync();
		}
		public async Task<int> GetTotalReviewsCount()
		{
			return await _context.Reviews.CountAsync();
		}
		public async Task<IEnumerable<Review>> GetListReviewByRating(int rating)
		{
			return await _context.Reviews.Where(r => r.Rating.Equals(rating)).ToListAsync();
		}
		public async Task<IEnumerable<Review>> GetListReviewByWeek()
		{
			DateTime oneWeekAgo = DateTime.Now.AddDays(-7);
			return await _context.Reviews
								 .Where(review => review.CreatedAt >= oneWeekAgo)
								 .ToListAsync();
		}

		public async Task<IEnumerable<Review>> GetAllReviewByProduct(Guid productId)
		{
			return await _context.Reviews.Where(r => r.ProductId.Equals(productId) && r.Status == true).ToListAsync();
		}
		public async Task AddReview(Review review)
		{
			await _context.Reviews.AddAsync(review);
		}

		public async Task SoftDeleteReview(int reviewId)
		{
			var reviewToDelete = await _context.Reviews.FirstOrDefaultAsync(r => r.Id.Equals(reviewId));
			if (reviewToDelete != null)
			{
				reviewToDelete.Status = false;
			}
		}

		public async Task HardDeleteReview(int reviewId)
		{
			var reviewToDelete = await _context.Reviews.FirstOrDefaultAsync(r => r.Id.Equals(reviewId));
			if (reviewToDelete != null)
			{
				_context.Reviews.Remove(reviewToDelete);
			}
		}

		public async Task RestoreReview(int reviewId)
		{
			var reviewToDelete = await _context.Reviews.FirstOrDefaultAsync(r => r.Id.Equals(reviewId));
			if (reviewToDelete != null)
			{
				reviewToDelete.Status = true;
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
