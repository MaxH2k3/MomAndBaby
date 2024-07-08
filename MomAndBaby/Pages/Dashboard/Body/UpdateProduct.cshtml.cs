using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;

namespace MomAndBaby.Pages.Dashboard.Body;

public class UpdateProduct : PageModel
{
    [BindProperty]
    public ProductDto EditProduct { get; set; }

    private readonly IProductService _productService;

    public UpdateProduct(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task OnGet(Guid productId)
    {
        EditProduct = await _productService.GetById(productId);
    }

    public async Task<IActionResult> OnPostSave()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        try
        {
            var result = await _productService.UpdateProduct(EditProduct);
            if (!result)
            {
                TempData["ErrorMessage"] = "Product updated failed.";
                return Page();
            }
            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToPage("AddProduct");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return Page();
        }
    }
}