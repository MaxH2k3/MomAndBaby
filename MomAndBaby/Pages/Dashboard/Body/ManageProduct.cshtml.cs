using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Dashboard.Body;

public class ManageProduct : PageModel
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public List<ProductDto> Products { get; set; }

    public ManageProduct(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task OnGet()
    {
        var products = await _productService.GetAll();
        Products = _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<IActionResult> OnPostDelete(Guid productId)
    {
        var result = await _productService.SoftDeleteProduct(productId);
        if (!result)
        {
            TempData["ErrorMessage"] = "Delete product failed.";
            return Page();
        }
        TempData["SuccessMessage"] = "Delete product successfully.";
        return RedirectToPage("ManageProduct");
    }
}