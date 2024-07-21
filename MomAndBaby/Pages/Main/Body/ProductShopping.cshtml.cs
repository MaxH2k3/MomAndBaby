using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.CartSessionModel;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using MomAndBaby.Service.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MomAndBaby.Pages.Main.Body
{
    [IgnoreAntiforgeryToken]
    public class ProductShoppingModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProductShoppingModel(IProductService productService, ICategoryService categoryService, IUserService userService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userService = userService;
            _mapper = mapper;
        }

        [BindProperty]
        public IEnumerable<Product> Products { get; set; }
        public int TotalProductsCount { get; set; }
        public int FilteredProductsCount { get; set; }
        public IEnumerable<ProductCategoryDto> ProductCategoryDto { get; set; }
        public IEnumerable<ProductOriginalDto> ProductOriginalDto { get; set; }
        public IEnumerable<ProductCompanyDto> ProductCompanyDto { get; set; }

        //[BindProperty]
        //public int RoleId { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _productService.GetAll();
            TotalProductsCount = Products.Count();
            FilteredProductsCount = TotalProductsCount;

            ProductCategoryDto = await _productService.GetCategoryShopping();
            ProductCompanyDto = await _productService.GetCompanyShopping();
            ProductOriginalDto = await _productService.GetOriginalShopping();

            
            

        }

        public async Task<IActionResult> OnGetAddToCartAsync(Guid productId)
        {
            var product = await _productService.GetById(productId);
            if (product != null)
            {
                var carts = GetCart();
                var cart = _mapper.Map<CartSessionModel>(product);

                var cartItem = carts.FirstOrDefault(x => x.Id == productId);
                if (cartItem != null)
                {
                    cartItem.NumberOfProduct++;
                } else
                {
                    cart.NumberOfProduct = 1;
                    carts.Add(cart);
                }

                SaveCart(carts);
            
            }
           
           return RedirectToPage();
        }

        private List<CartSessionModel> GetCart()
        {
            var cart = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cart))
            {
                return new List<CartSessionModel>();
            }
            return JsonConvert.DeserializeObject<List<CartSessionModel>>(cart);
        }

        private void SaveCart(List<CartSessionModel> cart)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }
}
