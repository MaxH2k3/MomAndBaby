using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MomAndBaby.BusinessObject.Models.ProductDto;

public class ImageAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value is IFormFile { Length: > 0 };
    }
}