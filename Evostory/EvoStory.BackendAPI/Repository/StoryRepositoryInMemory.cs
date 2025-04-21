using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory : IStoryRepository
    {
        private List<Story> stories = new();
        public void CreateStory(Story story)
        {
            stories.Add(story);
        }

        public void DeleteStory(Guid storyId)
        {
            var result = stories.FirstOrDefault(story => story.Id == storyId);
            if (result == null)
            {
                throw new Exception("Not Found");
            }
            stories.Remove(result);
        }

        public Story GetStory(Guid storyId)
        {
            var result = stories.FirstOrDefault(story => story.Id == storyId);
            if (result == null)
            {
                throw new Exception("Not Found");
            }
            return result;
        }

        public IEnumerable<Story> GetStories()
        {
            return stories;
        }

        public void EditStory(Story story)
        {
            var result = stories.FirstOrDefault(story => story.Id == story.Id);
            if (result == null)
            {
                throw new Exception("Not Found");
            }
            stories.Remove(result);
            stories.Add(story);
        }
    }
}
