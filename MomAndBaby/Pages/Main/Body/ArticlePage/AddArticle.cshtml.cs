using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Repository;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;

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
        public ArticleDTO ArticleDTO { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
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
            };

            _articleService.AddArticle(articleToCreate);

            return Redirect("/article");
        }
    }
}
