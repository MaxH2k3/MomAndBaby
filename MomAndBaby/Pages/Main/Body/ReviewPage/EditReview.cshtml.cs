using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service;
using Microsoft.EntityFrameworkCore;

namespace MomAndBaby.Pages.Main.Body.ReviewPage
{
    public class EditReviewModel : PageModel
    {
		[BindProperty]
		public Review Review { get; set; }

		private readonly IReviewService _reviewService;

		public EditReviewModel(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		public async Task<IActionResult> OnGet(int reviewId, Guid productId)
		{
			if (productId == Guid.Empty)
			{
				return Redirect("/");
			}
			Review = await _reviewService.GetReviewById(reviewId);
			if (Review == null)
			{
				return Redirect("/product_detail?productId=" + productId);
			}
			if (User.GetUserIdFromToken().ToString() != Review.UserId.ToString())
			{
				return Redirect("/product_detail?productId=" + productId);
			}
			return Page();
		}

		public async Task<IActionResult> OnPostUpdateAsync(int reviewId, Guid productId)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			await _reviewService.EditReview(Review, reviewId);
			return Redirect("product_detail?productId=" + productId);
		}

		public async Task<IActionResult> OnPostDeleteAsync(int reviewId, Guid productId)
		{
			await _reviewService.DeleteReview(reviewId);

			// Redirect to the same page to refresh the list
			return Redirect("product_detail?productId=" + productId);
		}
	}
}
