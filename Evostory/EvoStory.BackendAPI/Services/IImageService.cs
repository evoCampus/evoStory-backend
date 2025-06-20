using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IImageService
    {
        IEnumerable<ImageDTO> GetAllImages();
        string? GetImage(Guid id);
    }
}
