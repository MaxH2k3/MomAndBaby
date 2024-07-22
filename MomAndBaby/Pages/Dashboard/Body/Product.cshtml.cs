using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.Service;
using MomAndBaby.Service.Worker;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body;

public class Product : PageModel
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly NotificationWorker _notificationWorker;

    [BindProperty]
    public Guid? ProductId { get; set; }
    [BindProperty]
    public ProductDto ProductDto { get; set; }
    [BindProperty]
    public IEnumerable<Category> Categories { get; set; }
    [BindProperty]
    public string? CategoryName { get; set; }

    [BindProperty]
    public IEnumerable<string> ProductOriginalName { get; set; }

    public Product(IProductService productService, ICategoryService categoryService, NotificationWorker notificationWorker)
    {
        _productService = productService;
        _categoryService = categoryService;
        _notificationWorker = notificationWorker;
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
        ProductOriginalName = await _productService.GetOriginalName();
    }

    public async Task<IActionResult> OnPostCreate()
    {
        Categories = await _categoryService.GetCategory();
        ProductOriginalName = await _productService.GetOriginalName();

        if (!ModelState.IsValid)
        {
            if (ProductDto.ImageFile is null || ProductDto.ImageFile.Length <= 0)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "Image is required!");
            }
            return Page();
        }
        
        try
        {
            ProductDto.Status = StatusConstraint.AVAILABLE;
            var result = await _productService.CreateProduct(ProductDto);
            if (!result)
            {
                return Page();
            }
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Product, NotificationType.Added);
            return RedirectToPage("ListProduct");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(ex.Message == "Product name already exists." ? "ProductDto.Name" : "", ex.Message);
            return Page();
        }
    }

    public async Task<IActionResult> OnPostUpdate()
    {
        Categories = await _categoryService.GetCategory();
        ProductOriginalName = await _productService.GetOriginalName();

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
                return Page();
            }
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Product, NotificationType.Modified);
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
        Categories = await _categoryService.GetCategory();
        ProductOriginalName = await _productService.GetOriginalName();

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
                return Page();
            }
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Product, NotificationType.Modified);
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
        Categories = await _categoryService.GetCategory();
        ProductOriginalName = await _productService.GetOriginalName();

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
                return Page();
            }
            return RedirectToPage("ListProduct");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(ex.Message == "Product name already exists." ? "ProductDto.Name" : "", ex.Message);
            return Page();
        }
    }

    public async Task<IActionResult> OnPostCreateCategory()
    {
        Categories = await _categoryService.GetCategory();
        

        if (string.IsNullOrWhiteSpace(CategoryName))
        {
            TempData["CategoryValid"] = "Category is required.";
            return Page();
        }

        var check = await _categoryService.NameExistAsync(CategoryName);
        
        if (check)
        {
            TempData["CategoryValid"] = "This category already exists.";
            return Page();
        }

        var result = await _categoryService.AddCategory(CategoryName);
        if (result)
        {
            Categories = await _categoryService.GetCategory();
        }
        return Page();
    }
    
    public IActionResult OnPostCancel()
    {
        return RedirectToPage("ListProduct");
    }
    
}