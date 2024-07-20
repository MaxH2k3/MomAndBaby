using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body.ArticlePage
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleService _articleService;

        public ArticleModel(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public PaginatedList<Article> Articles { get; set; }

        public async Task OnGet(int pageIndex = 1)
        {
            int pageSize = 5;
            Articles = await _articleService.GetListArticle(pageIndex, pageSize);
        }
    }
}
