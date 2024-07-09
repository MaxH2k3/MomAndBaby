using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MomAndBaby.BusinessObject.Models.ProductDto;

public class ProductDto
{
    [Required(ErrorMessage = "Product Name is required!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Description is required!")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Price is required!")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Category is required!")]
    public string? Category { get; set; }
    [Required(ErrorMessage = "Stock is required!")]
    public int Stock { get; set; }
    // [Required(ErrorMessage = "Product Name is required!")]
    public IFormFile? ImageFile { get; set; }
    public string? Status { get; set; }
}