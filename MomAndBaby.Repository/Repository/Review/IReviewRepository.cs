using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Repository
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> getAllReviewByProduct(Guid productId);
        public void AddReview(Review review);
        public void DeleteReview(int reviewId);
    }
}
