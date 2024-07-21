using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.Worker;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListPostModel : PageModel
    {
        private readonly IArticleService _articleService;
		private readonly NotificationWorker _notificationWorker;

        public ListPostModel(IArticleService articleService, NotificationWorker notificationWorker)
        {
            _articleService = articleService;
            _notificationWorker = notificationWorker;
        }
        public PaginatedList<Article> Articles { get; set; }

        public async Task OnGet(string searchValue = "", int pageIndex = 1)
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.PostList;
			searchValue = searchValue ?? "";
			TempData["search"] = searchValue;
			int pageSize = 5;
            Articles = await _articleService.GetListArticle(pageIndex, pageSize, searchValue);
        }

		public async Task<IActionResult> OnPostSoftDelete(int articleId)
		{
			await _articleService.SoftDeleteArticle(articleId);
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Article, NotificationType.Modified);
            return Redirect("/dashboard/list-post");
		}

		public async Task<IActionResult> OnPostHardDelete(int articleId)
		{
			await _articleService.HardDeleteArticle(articleId);
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Article, NotificationType.Deleted);
            return Redirect("/dashboard/list-post");
		}

        public async Task<IActionResult> OnPostRestore(int articleId)
        {
			await _articleService.RestoreArticle(articleId);
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Article, NotificationType.Modified);
            return Redirect("/dashboard/list-post");
		}
	}
}
