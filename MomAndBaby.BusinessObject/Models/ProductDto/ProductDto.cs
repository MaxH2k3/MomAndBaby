using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MomAndBaby.BusinessObject.Models.ProductDto;

public class ProductDto
{
    public Guid? Id { get; set; }
    [IsNullOrWhiteSpace(ErrorMessage = "Product Name is required!")]
    public string? Name { get; set; }
    [IsNullOrWhiteSpace(ErrorMessage = "Description is required!")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Price is required!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public decimal? Price { get; set; }
    [IsNullOrWhiteSpace(ErrorMessage = "Category is required!")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Category must be between 1 and 50 characters.")]
    public string? Category { get; set; }
    [Required(ErrorMessage = "Stock is required!")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative integer.")]
    public int? Stock { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? Image { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
}