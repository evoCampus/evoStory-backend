using Evostory.Story.Models;
namespace EvoStory.BackendAPI.Database
{
    public class DatabaseInMemory() : IDatabase
    {
        public Dictionary<Guid, Story> Stories { get; set; } = new();
    }
}