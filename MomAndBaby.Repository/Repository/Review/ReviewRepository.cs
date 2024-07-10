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

		public IEnumerable<Review> getAllReviewByProduct(Guid productId)
		{
			return _context.Reviews.Where(r => r.ProductId.Equals(productId) && r.Status == true).ToList();
		}
		public void AddReview(Review review)
		{
			_context.Reviews.Add(review);
			_context.SaveChanges();
		}

		public void DeleteReview(int reviewId)
		{
			var reviewToDelete = _context.Reviews.FirstOrDefault(r => r.Id.Equals(reviewId));
			_context.Reviews.Remove(reviewToDelete);
			_context.SaveChanges();
		}
	}
}
