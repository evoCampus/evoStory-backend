using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;
using EvoStory.BackendAPI.Exceptions;

namespace EvoStory.BackendAPI.Repository
{
    public class ImageRepositoryFileSystem(ILogger<ImageRepositoryFileSystem> logger) : IImageRepository
    {
        public Dictionary<Guid, string> _images { get; set; } = new()
        {
            { Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Images/testimage.png" },
            { Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Images/image2.jpg" }
        };

        public IEnumerable<Image> GetAllImages()
        {
            logger.LogTrace("GetAllImages repository method called.");
            return _images.Select(image => new Image
            {
                Id = image.Key,
                Path = Path.GetFileName(image.Value),
            });
        }

        public byte[]? GetImage(Guid imageId)
        {
            logger.LogTrace("GetImage repository was called.");
            if (_images.TryGetValue(imageId, out var path) && File.Exists(path)) 
            {
                return File.ReadAllBytes(path);
            }
            logger.LogWarning($"Image with Id: {imageId} was not found.");
            throw new RepositoryException($"No Image with ID {imageId} found.");
        }
    }
}
