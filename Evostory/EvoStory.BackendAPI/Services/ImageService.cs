using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;
using Microsoft.Extensions.Logging;

namespace EvoStory.BackendAPI.Services
{
    public class ImageService(IImageRepository imageRepository, IDTOConversionService dTOConversion, ILogger<ImageService> logger) : IImageService
    {
        public IEnumerable<ImageDTO> GetAllImages()
        {
            logger.LogDebug("Get images service was called.");
            var result = imageRepository.GetAllImages();
            var imagesDTO = result.Select(image => dTOConversion.ConvertImageToImageDTO(image));
            return imagesDTO;
        }

        public string? GetImage(Guid id)
        {
            logger.LogDebug("Get image service was called.");
            var imageBytes = imageRepository.GetImage(id);
            var base64Image = Convert.ToBase64String(imageBytes);
            return base64Image;
        }
    }
}
