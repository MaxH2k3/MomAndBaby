using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using MomAndBaby.Service.Service;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body;

public class Product : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    
    [BindProperty]
    public Guid? ProductId { get; set; }
    
    [BindProperty]
    public ProductDto ProductDto { get; set; }
    [BindProperty]
    public IEnumerable<Category> Categories { get; set; }

    public Product(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task OnGet(Guid? productId)
    {
        ViewData[VariableConstant.CurrentMenu] = (int)Menu.ProductAdd;
        ProductId = productId;
        if (ProductId.HasValue)
        {
            ProductDto = await _productService.GetById((Guid)productId);
        }
        Categories = await _categoryService.GetCategory();
    }

    public async Task<IActionResult> OnPostCreate()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        try
        {
            ProductDto.Status = StatusConstraint.AVAILABLE;
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
            switch (ex.Message)
            {
                case "Image is required!":
                    ModelState.AddModelError("ProductDto.ImageFile", ex.Message);
                    break;
                case "Product name already exists.":
                    ModelState.AddModelError("ProductDto.Name", ex.Message);
                    break;
                default:
                    ModelState.AddModelError("", ex.Message);
                    break;
            }

            return Page();
        }
    }

    public async Task<IActionResult> OnPostUpdate()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        try
        {
            ProductDto.Status = StatusConstraint.AVAILABLE;
            var result = await _productService.UpdateProduct(ProductDto);
            if (!result)
            {
                TempData["ErrorMessage"] = "Product updated failed.";
                return Page();
            }
            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToPage("ListProduct");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(ex.Message == "Product name already exists." ? "ProductDto.Name" : "", ex.Message);
            return Page();
        }
    }

    public async Task<IActionResult> OnPostHidden()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        try
        {
            ProductDto.Status = StatusConstraint.HIDDEN;
            var result = await _productService.UpdateProduct(ProductDto);
            if (!result)
            {
                TempData["ErrorMessage"] = "Product updated failed.";
                return Page();
            }
            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToPage("ListProduct");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(ex.Message == "Product name already exists." ? "ProductDto.Name" : "", ex.Message);
            return Page();
        }
    }
    
    public async Task<IActionResult> OnPostSaveDraft()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        try
        {
            ProductDto.Status = StatusConstraint.DRAFT;
            var result = await _productService.UpdateProduct(ProductDto);
            if (!result)
            {
                TempData["ErrorMessage"] = "Product updated failed.";
                return Page();
            }
            TempData["SuccessMessage"] = "Product updated successfully.";
            return RedirectToPage("ListProduct");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(ex.Message == "Product name already exists." ? "ProductDto.Name" : "", ex.Message);
            return Page();
        }
    }
 
    public IActionResult OnPostCancel()
    {
        return RedirectToPage("ListProduct");
    }
    
}