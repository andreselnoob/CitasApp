using CloudinaryDotNet.Actions;
namespace CitasApp.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsyc(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
