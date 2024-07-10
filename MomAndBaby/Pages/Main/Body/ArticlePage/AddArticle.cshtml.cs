using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Repository;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body.ArticlePage
{
    public class AddArticleModel : PageModel
    {
        private readonly IArticleService _articleService;

        public AddArticleModel(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [BindProperty]
        public Article Article { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Article.CreatedAt = DateTime.Now;

            _articleService.AddArticle(Article);

            return Redirect("/article");
        }
    }
}
