using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Dashboard.Body;

public class AddProduct : PageModel
{
    private readonly IProductService _productService;
    
    [BindProperty]
    public ProductDto ProductDto { get; set; }
    
    public AddProduct(IProductService productService)
    {
        _productService = productService;
    }


    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostSave()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        try
        {
            var result = await _productService.CreateProduct(ProductDto);
            if (!result)
            {
                TempData["ErrorMessage"] = "Product created failed.";
                return Page();
            }
            TempData["SuccessMessage"] = "Product created successfully.";
            return Page();
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return Page();
        }
    }
}