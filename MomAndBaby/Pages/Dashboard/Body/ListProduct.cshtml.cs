using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using MomAndBaby.Service.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.Worker;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListProductModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly NotificationWorker _notificationWorker;
        private readonly ICategoryService _categoryService;
        
        [BindProperty]
        public int? IsDelete { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 8;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<ProductDto> Data { get; set; }
        
        public List<Category> Categories { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        
        public ListProductModel(IProductService productService, ICategoryService categoryService, NotificationWorker notificationWorker)
        {
            _productService = productService;
            _categoryService = categoryService;
            _notificationWorker = notificationWorker;
        }

        public async Task OnGet(string searchValue, int? categoryDelete)
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.ProductList;
            IsDelete = categoryDelete;
            TempData["search"] = searchValue;

            var products = await _productService.GetAllAdmin(CurrentPage, searchValue);
            Count = products.Item1;
            Data = products.Item2;
            var categories = await _categoryService.GetCategory();
            Categories = categories.ToList();
        }
        
        public async Task<IActionResult> OnPostDelete(Guid productId)
        {
            var categories = await _categoryService.GetCategory();
            Categories = categories.ToList();
            var result = await _productService.SoftDeleteProduct(productId);
            if (!result)
            {
                return Page();
            }
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Product, NotificationType.Modified);
            return RedirectToPage("ListProduct");
        }
        
        public async Task<IActionResult> OnPostDeleteCategory(int categoryId)
        {
            try
            {
                await _categoryService.DeleteCategory(categoryId);
                return RedirectToPage("ListProduct", new { categoryDelete = 1 });
            }
            catch (ArgumentException e)
            {
                return RedirectToPage("ListProduct", new { categoryDelete = 2 }); 
            }
            
        }
    }
}
