using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
		productReview = await _reviewService.GetAllReviewByProduct(productId);
		ProductAverageRating = await _reviewService.GetAverageRating(productId);
		foreach (var review in productReview)
		{
			review.User = await _userService.getUserById(review.UserId);
		}
	}
}
