using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IReviewRepository
    {
		public Task<IEnumerable<Review>> GetAllReview();
		public Task<IEnumerable<Review>> GetListReview(int pageNumber, int pageSize);
		public Task<int> GetTotalReviewsCount();
        public Task<IEnumerable<Review>> GetListReviewByRating(int rating);
		public Task<IEnumerable<Review>> GetAllReviewByProduct(Guid productId);
		public Task<IEnumerable<Review>> GetListReviewByWeek();
		public Task<Review> GetReviewById(int id);
        public Task AddReview(Review review);
        public Task EditReview(Review review, int reviewId);
        public Task SoftDeleteReview(int reviewId);
        public Task HardDeleteReview(int reviewId);
        public Task RestoreReview(int reviewId);
    }
}
