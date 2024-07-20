using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListPostModel : PageModel
    {
        private readonly IArticleService _articleService;

        public ListPostModel(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public PaginatedList<Article> Articles { get; set; }

        public async Task OnGet(int pageIndex = 1)
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.PostList;
            int pageSize = 5;
            Articles = await _articleService.GetListArticle(pageIndex, pageSize, "adva");
        }

		public async Task<IActionResult> OnPostSoftDelete(int articleId)
		{
			await _articleService.SoftDeleteArticle(articleId);
			return Redirect("/dashboard/list-post");
		}

		public async Task<IActionResult> OnPostHardDelete(int articleId)
		{
			await _articleService.HardDeleteArticle(articleId);
			return Redirect("/dashboard/list-post");
		}

        public async Task<IActionResult> OnPostRestore(int articleId)
        {
			await _articleService.RestoreArticle(articleId);
			return Redirect("/dashboard/list-post");
		}
	}
}
