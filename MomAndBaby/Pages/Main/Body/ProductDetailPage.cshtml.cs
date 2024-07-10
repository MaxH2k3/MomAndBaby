using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Main.Body;

public class ProductDetailPage : PageModel
{
    [BindProperty]
    public ProductDto Product { get; set; }
    public IEnumerable<ProductDto> Products { get; set; }
    private readonly IProductService _productService;
    public ProductDetailPage(IProductService productService)
    {
        _productService = productService;
    }
    public async Task OnGet(Guid productId)
    {
        ViewData["Title"] = "Product";
        var product = await _productService.GetById(productId);
        var products = await _productService.GetRelatedProducts(product.CategoryId);
        Product = product;
        Products = products;
    }
}