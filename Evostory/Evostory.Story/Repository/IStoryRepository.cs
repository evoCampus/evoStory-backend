using EvoStory.Database.Models;
using System.Threading.Tasks;

namespace EvoStory.Database.Repository
{
    public interface IStoryRepository
    {
        Task<Story> CreateStory(Story story);
        Task<Story> GetStory(Guid storyId);
        Task<IEnumerable<Story>> GetStories();
        Task<Story> EditStory(Story story);
        Task<Story> DeleteStory(Guid storyId);
    }
}
