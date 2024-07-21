using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.Worker;
using MomAndBaby.Utilities.Constants;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListProductModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly NotificationWorker _notificationWorker;
        public List<ProductDto> Products { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 8;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<ProductDto> Data { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        
        public ListProductModel(IProductService productService, NotificationWorker notificationWorker)
        {
            _productService = productService;
            _notificationWorker = notificationWorker;
        }
        
        public async Task OnGet(string searchValue)
        {
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.ProductList;
            
            TempData["search"] = searchValue;

            var products = await _productService.GetAllAdmin(CurrentPage, searchValue);
            Count = products.Item1;
            Data = products.Item2;
            // Products = products.ToList();
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
            _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Product, NotificationType.Modified);
            return RedirectToPage("ListProduct");
        }
    }
}
