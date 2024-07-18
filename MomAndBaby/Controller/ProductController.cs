using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.ProductDto;
using MomAndBaby.Service;
using Newtonsoft.Json;

namespace MomAndBaby.Controller
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("filter")]
        public async Task<ProductShoppingDto> FilterProducts(int? categoryId, string? companyName, string? original, decimal? startPrice, decimal? endPrice, int? numOfStars, string? sortCriteria, int page, int pageSize = 9)
        {
            var filteredProducts = await _productService.GetFilteredProducts(categoryId, companyName, original, startPrice, endPrice, numOfStars, sortCriteria);
            var allProduct = await _productService.GetAll();


            var pagedProducts = filteredProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var productJson = JsonConvert.SerializeObject(pagedProducts, jsonSettings);

            var response = new ProductShoppingDto
            {
                Products = productJson,
                TotalProductsCount = allProduct.Count(),
                FilteredProductsCount = filteredProducts.Count()
            };
            return response;
        }

        [HttpGet("")]
        public async Task<IEnumerable<ProductPreview>> GetProducts()
        {
            var products = await _productService.GetAll();

            return _mapper.Map<IEnumerable<ProductPreview>>(products);
        }

    }
}
