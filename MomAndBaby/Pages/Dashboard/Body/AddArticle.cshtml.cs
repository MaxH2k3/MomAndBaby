using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Repository;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.Service.Cloudinary;
using MomAndBaby.Service.Worker;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
	public class AddArticleModel : PageModel
	{
		private readonly IArticleService _articleService;
		private readonly NotificationWorker _notificationWorker;
		private readonly ICloudinaryService _cloudinaryService;

		public AddArticleModel(IArticleService articleService, NotificationWorker notificationWorker, ICloudinaryService cloudinaryService)
		{
			_articleService = articleService;
			_notificationWorker = notificationWorker;
			_cloudinaryService = cloudinaryService;
		}

		[BindProperty]
		public ArticleDTO ArticleDTO { get; set; }
		public IActionResult OnGet()
		{
			ViewData[VariableConstant.CurrentMenu] = (int)Menu.PostAdd;
			var userRole = User.Claims.FirstOrDefault(u => u.Type.Equals(UserClaimType.Role)).Value.ToString().ToLower();
            if (userRole != "2" || userRole != "1")
			{
				return Redirect("/article");
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(IFormFile ImageUpload)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var image = await _cloudinaryService.UploadAsync(ImageUpload);

			Article articleToCreate = new Article
			{
				AuthorId = Guid.Parse(User.Claims.FirstOrDefault(u => u.Type.Equals(UserClaimType.UserId))?.Value.ToString()),
				Title = ArticleDTO.Title,
				Content = ArticleDTO.Content,
				CreatedAt = DateTime.Now,
				Image = image.Url.ToString(),
				Status = true
			};

			await _articleService.AddArticle(articleToCreate);
			//var articleJustCreated = await _articleService.GetNewestArticle();
			//int newArticleId = articleJustCreated.Id;

			//if (ImageUpload != null && ImageUpload.Length > 0)
			//{
			//	var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/article-image", $"{newArticleId}.jpg");

			//	using (var stream = new FileStream(filePath, FileMode.Create))
			//	{
			//		await ImageUpload.CopyToAsync(stream);
			//	}
			//}

			return Redirect("/article");
		}
	}
}
