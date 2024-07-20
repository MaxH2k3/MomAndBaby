using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body.ArticlePage
{
	public class ArticleDetailModel : PageModel
	{
		private readonly IArticleService _articleService;
		private readonly IUserService _userService;

		public ArticleDetailModel(IArticleService articleService, IUserService userService)
		{
			_articleService = articleService;
			_userService = userService;
		}

		public ArticleDTO ArticleDTO { get; set; }

		public async Task OnGet(int articleId)
		{
			ArticleDTO = await _articleService.GetArticleDTOById(articleId);
		}

		public async Task<IActionResult> OnPostDelete(int articleId)
		{
			await _articleService.SoftDeleteArticle(articleId);
			return Redirect("/article");
		}
	}
}
