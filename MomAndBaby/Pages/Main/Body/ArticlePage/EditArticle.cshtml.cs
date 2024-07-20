using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
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

        public async Task<IActionResult> OnGet(int articleId)
        {
			Article = await _articleService.GetArticleById(articleId);

			//if (User.Claims.FirstOrDefault(u => u.Type.Equals(UserClaimType.UserId))?.Value != Article.AuthorId.ToString())
   //         {
   //             return Redirect("/article");
   //         }
            if (Article == null)
            {
                return RedirectToPage("/article");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int articleId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var id = articleId;
            
            await _articleService.UpdateArticle(Article, articleId);
            return Redirect("/article-detail?articleId=" + id);
        }
    }
}
