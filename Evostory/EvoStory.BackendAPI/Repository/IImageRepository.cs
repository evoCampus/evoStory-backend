using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IImageRepository
    {
        Dictionary<Guid, string> _images { get; set; }
        IEnumerable<Image> GetAllImages();
        byte[]? GetImage(Guid id);
    }
}
