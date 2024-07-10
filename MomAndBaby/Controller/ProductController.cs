using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        

        [HttpGet("filter")]
        public async Task<ProductShoppingDto> FilterProducts(int? categoryId, decimal? startPrice, decimal? endPrice, int? numOfStars, string? sortCriteria)
        {
            var products = await _productService.GetFilteredProducts(categoryId, startPrice, endPrice, numOfStars, sortCriteria);
            var allProduct = await _productService.GetAll();


            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var productJson = JsonConvert.SerializeObject(products, jsonSettings);
            var response = new ProductShoppingDto
            {
                Products = productJson,
                TotalProductsCount = allProduct.Count(),
                FilteredProductsCount = products.Count()

            };
            return response;
        }

    }
}
