using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body
{
    public class ArticleModel : PageModel
    {
        public IEnumerable<Article> articles;

        private readonly IArticleService _articleService;

        public ArticleModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public void OnGet()
        {
            articles = _articleService.GetListArticle();
        }
    }
}
