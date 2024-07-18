using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListProductModel : PageModel
    {
        private readonly IProductService _productService;
        public List<ProductDto> Products { get; set; }
        
        public ListProductModel(IProductService productService)
        {
            _productService = productService;
        }
        
        public async Task OnGet(string searchValue)
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.ProductList;
            
            TempData["search"] = searchValue;

            var products = await _productService.GetAllAdmin(searchValue);
            Products = products.ToList();
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
            return RedirectToPage("ListProduct");
        }
    }
}
