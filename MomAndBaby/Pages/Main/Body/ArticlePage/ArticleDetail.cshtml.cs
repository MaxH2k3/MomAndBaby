using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Entity.DTO;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body.ArticlePage
{
    public class ArticleDetailModel : PageModel
    {
        private readonly IArticleService _articleService;

        public ArticleDetailModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public ArticleDTO Article { get; set; }

        public void OnGet(int articleId)
        {
            Article = _articleService.GetArticleDTOById(articleId);
        }

        public IActionResult OnPostDelete(int articleId)
        {
            _articleService.DeleteArticle(articleId);
            return Redirect("/article");
        }
    }
}
