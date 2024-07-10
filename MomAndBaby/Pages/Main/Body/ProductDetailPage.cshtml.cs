using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body;

public class ProductDetailPage : PageModel
{
	[BindProperty]
	public ProductDto ProductDto { get; set; }
	[BindProperty]
	public IEnumerable<Review> productReview { get; set; }
	[BindProperty]
	public float ProductAverageRating { get; set; }
	[BindProperty]
	public Review Review { get; set; }
	private readonly IProductService _productService;
	private readonly IReviewService _reviewService;
	private readonly IUserService _userService;

	public ProductDetailPage(IProductService productService, IReviewService reviewService, IUserService userService)
	{
		_productService = productService;
		_reviewService = reviewService;
		_userService = userService;
	}
	public async Task OnGet(Guid productId)
	{
		ProductDto = await _productService.GetById(productId);
		productReview = _reviewService.getAllReviewByProduct(productId);
		ProductAverageRating = _reviewService.getAverageRating(productId);
		foreach (var review in productReview)
		{
			review.User = await _userService.getUserById(review.UserId);
		}
	}

	public IActionResult OnPost()
	{
		//Review.ProductId = ProductDto.Id;
		Review.CreatedAt = DateTime.Now;
		Review.Status = true;
		_reviewService.AddReview(Review);
		return Page();
	}
}
