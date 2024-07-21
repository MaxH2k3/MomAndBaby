using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.BusinessObject.Models.ReviewDTO;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.Worker;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
	public class ReviewModel : PageModel
	{
		private readonly IReviewService _reviewService;
		private readonly NotificationWorker _notificationWorker;

        public ReviewModel(IReviewService reviewService, NotificationWorker notificationWorker)
		{
			_reviewService = reviewService;
            _notificationWorker = notificationWorker;

        }
		public PaginatedList<ReviewDTO> Reviews { get; set; }
		public float AverageRating { get; set; }
		public float ReviewCount {  get; set; }
		public float Total5StarRating {  get; set; }
		public float Total4StarRating {  get; set; }
		public float Total3StarRating {  get; set; }
		public float Total2StarRating {  get; set; }
		public float Total1StarRating {  get; set; }
		public float PreviousWeekReviewCount {  get; set; }
		public async Task OnGet(int pageIndex = 1)
		{
			ViewData[VariableConstant.CurrentMenu] = (int)Menu.Review;
			int pageSize = 5;
			AverageRating = await _reviewService.GetAverageRatingAllReview();
			ReviewCount = await _reviewService.ReviewCount();
			Total5StarRating = await _reviewService.GetListReviewByRating(5);
			Total4StarRating = await _reviewService.GetListReviewByRating(4);
			Total3StarRating = await _reviewService.GetListReviewByRating(3);
			Total2StarRating = await _reviewService.GetListReviewByRating(2);
			Total1StarRating = await _reviewService.GetListReviewByRating(1);
			PreviousWeekReviewCount = await _reviewService.GetListReviewLastWeek();
			Reviews = await _reviewService.GetListReviewDTO(pageIndex, pageSize);
		}

		public async Task<IActionResult> OnPostSoftDelete(int reviewId)
        {
			await _reviewService.SoftDeleteReview(reviewId);
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Review, NotificationType.Modified);
            return Redirect("/dashboard/list-review");
		}

		public async Task<IActionResult> OnPostHardDelete(int reviewId)
		{
			await _reviewService.HardDeleteReview(reviewId);
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Review, NotificationType.Deleted);
            return Redirect("/dashboard/list-review");
		}

		public async Task<IActionResult> OnPostRestore(int reviewId)
		{
			await _reviewService.RestoreReview(reviewId);
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Review, NotificationType.Modified);
            return Redirect("/dashboard/list-review");
		}
	}
}
