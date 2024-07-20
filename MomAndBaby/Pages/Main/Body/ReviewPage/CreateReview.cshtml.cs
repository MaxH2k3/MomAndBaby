using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using System;
using System.Threading.Tasks;

namespace MomAndBaby.Pages.Main.Body
{
	public class CreateReviewModel : PageModel
	{
		[BindProperty]
		public Review Review { get; set; }

		private readonly IReviewService _reviewService;

		public CreateReviewModel(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		public IActionResult OnGet(Guid productId)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return Redirect("/product_detail?productId=" + productId);
			}
			if (productId == Guid.Empty)
			{
				return Redirect("/");
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(Guid productId)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			string id = productId.ToString();

			Review.UserId = Guid.Parse(User.GetUserIdFromToken());
			Review.ProductId = productId;
			Review.CreatedAt = DateTime.Now;
			Review.Status = true;
			await _reviewService.AddReview(Review);
			return Redirect("product_detail?productId=" + id);
		}
	}
}
