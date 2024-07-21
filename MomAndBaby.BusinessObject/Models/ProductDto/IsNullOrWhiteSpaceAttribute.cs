using System.ComponentModel.DataAnnotations;

namespace MomAndBaby.BusinessObject.Models.ProductDto;

public class IsNullOrWhiteSpaceAttribute : ValidationAttribute
{
    public override bool IsValid(object value) 
    {
        var strValue = value as string;
        return !string.IsNullOrWhiteSpace(strValue);
    }
}