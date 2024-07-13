using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IReviewRepository
    {
        public Task<IEnumerable<Review>> GetAllReviewByProduct(Guid productId);
        public Task<Review> GetReviewById(int id);
        public Task AddReview(Review review);
        public Task EditReview(Review review, int reviewId);
        public Task DeleteReview(int reviewId);
    }
}
