using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace MomAndBaby.Service.Service.Cloudinary;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadAsync(IFormFile file);

}