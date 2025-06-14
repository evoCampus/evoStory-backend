using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Database
{
    public interface IDatabase
    {
        Dictionary<Guid, Story> Stories { get; set; }
    }
}
