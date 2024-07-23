using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.Service;
using MomAndBaby.Service.Service.Cloudinary;

namespace MomAndBaby.Pages.Main.Body.ArticlePage
{
    public class EditArticleModel : PageModel
    {
        private readonly IArticleService _articleService;
		private readonly ICloudinaryService _cloudinaryService;

		public EditArticleModel(IArticleService articleService, ICloudinaryService cloudinaryService)
		{
            _articleService = articleService;
			_cloudinaryService = cloudinaryService;
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

        public async Task<IActionResult> OnPost(int articleId, IFormFile ImageUpload)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var articleToUpdate = await _articleService.GetArticleById(articleId);
            if (articleToUpdate == null)
            {
                return RedirectToPage("/article");
            }

			var image = await _cloudinaryService.UploadAsync(ImageUpload);

			articleToUpdate.Title = Article.Title;
            articleToUpdate.Content = Article.Content;
            articleToUpdate.Image = image.Url.ToString();

            await _articleService.UpdateArticle(articleToUpdate, articleId);
            return Redirect("/article-detail?articleId=" + articleId);
        }

    }
}
