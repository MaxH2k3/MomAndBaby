using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Entity;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body.ArticlePage
{
    public class EditArticleModel : PageModel
    {
        private readonly IArticleService _articleService;

        public EditArticleModel(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [BindProperty]
        public Article Article { get; set; }

        public IActionResult OnGet(int articleId)
        {
            Article = _articleService.GetArticleById(articleId);
            if (Article == null)
            {
                return RedirectToPage("/article");
            }
            return Page();
        }

        public IActionResult OnPost(int articleId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _articleService.UpdateArticle(Article, articleId);
            return Redirect("/article-detail?articleId=" + Article.Id);
        }
    }
}
