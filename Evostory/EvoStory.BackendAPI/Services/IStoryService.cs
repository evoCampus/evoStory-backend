using EvoStory.BackendAPI.DTO;
using EvoStory.Database.Models;
using System.Threading.Tasks;

namespace EvoStory.BackendAPI.Services
{
    public interface IStoryService
    {
        Task<StoryDTO> CreateStory(CreateStoryDTO story);
        Task<StoryDTO> GetStory(Guid storyId);
        Task<IEnumerable<StoryDTO>> GetStories();
        Task<StoryDTO> EditStory(EditStoryDTO story);
        Task<StoryDTO> DeleteStory(Guid storyId);
    }
}
