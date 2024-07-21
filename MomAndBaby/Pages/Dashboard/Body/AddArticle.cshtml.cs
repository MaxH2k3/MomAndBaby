using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Repository;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class AddArticleModel : PageModel
    {
        private readonly IArticleService _articleService;

        public AddArticleModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [BindProperty]
        public ArticleDTO ArticleDTO { get; set; }
        public IActionResult OnGet()
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.PostAdd;
			if (User.Claims.FirstOrDefault(u => u.Type.Equals(UserClaimType.Role)).Value.ToString().ToLower() == "admin")
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

			Article articleToCreate = new Article
			{
				AuthorId = Guid.Parse(User.Claims.FirstOrDefault(u => u.Type.Equals(UserClaimType.UserId))?.Value.ToString()),
				Title = ArticleDTO.Title,
				Content = ArticleDTO.Content,
				CreatedAt = DateTime.Now,
				Status = true
			};

			await _articleService.AddArticle(articleToCreate);
			var articleJustCreated = await _articleService.GetNewestArticle();
			int newArticleId = articleJustCreated.Id;

			if (ImageUpload != null && ImageUpload.Length > 0)
			{
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/article-image", $"{newArticleId}.jpg");

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await ImageUpload.CopyToAsync(stream);
				}
			}

			return Redirect("/article");
		}
	}
}
