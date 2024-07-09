using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body;

public class ProductDetailPage : PageModel
{
    [BindProperty]
    public ProductDto ProductDto { get; set; }
    private readonly IProductService _productService;

    public ProductDetailPage(IProductService productService)
    {
        _productService = productService;
    }
    public async Task OnGet(Guid productId)
    {
        ProductDto = await _productService.GetById(productId);
        if (ProductDto != null)
        {
            return;
        }
    }
}