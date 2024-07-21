using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.BusinessObject.Models.ProductDto;

public class ProductDto
{
    public Guid? Id { get; set; }
    [IsNullOrWhiteSpace(ErrorMessage = "Product Name is required!")]
    public string Name { get; set; }
    [IsNullOrWhiteSpace(ErrorMessage = "Description is required!")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Price is required!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public decimal UnitPrice { get; set; }
    [Required(ErrorMessage = "Sell Price is required!")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Sell Price must be a positive number.")]
    public decimal PurchasePrice { get; set; }
    public int? CategoryId { get; set; }
    [Required(ErrorMessage = "Stock is required!")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative integer.")]
    public int Stock { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? Image { get; set; }
    [IsNullOrWhiteSpace(ErrorMessage = "Country is required!")]
    public string? Original { get; set; }
    [IsNullOrWhiteSpace(ErrorMessage = "Brand is required!")]
    public string? Company { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Category? CategoryNavigation { get; set; }
    public ProductStatistic? Statistic { get; set; }
}