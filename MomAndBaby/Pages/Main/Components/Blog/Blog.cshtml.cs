using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.Models.ProductModel;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Components.Blog;

public class Blog : ViewComponent
{
    private readonly IArticleService _articleService;

    public Blog(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var productStoreModel = new ProductStoreModel();
        productStoreModel.ListArticle = await _articleService.GetListActiveArticle(1, 3);
        return View("Blog", productStoreModel);
    }
}