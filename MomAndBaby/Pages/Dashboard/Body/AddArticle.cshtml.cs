using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models;
using MomAndBaby.Repository;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
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
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.PostAdd;
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
                Status = true
            };

            _articleService.AddArticle(articleToCreate);

            return Redirect("/article");
        }
    }
}
