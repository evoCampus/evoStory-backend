using Evostory.Story.Models;
using EvoStory.BackendAPI.Exceptions;

namespace EvoStory.BackendAPI.Repository
{
    public class StoryRepositoryInMemory : IStoryRepository
    {
        private Dictionary<Guid, Story> _stories = new();
        public Story CreateStory(Story story)
        {
            if (_stories.ContainsKey(story.Id))
            {
                throw new RepositoryException($"Existing story with {story.Id} found.");
            }
            _stories.Add(story.Id, story);
            return story;
        }

        public Story DeleteStory(Guid storyId)
        {
            var result = _stories.FirstOrDefault(story => story.Key == storyId);
            if (result.Value is null)
            {
                throw new RepositoryException($"No story with {storyId} found.");
            }

            _stories.Remove(result.Key);
            return result.Value;
        }

        public Story GetStory(Guid storyId)
        {
            var result = _stories.FirstOrDefault(story => story.Key == storyId);
            if (result.Value is null)
            {
                throw new RepositoryException($"No story with {storyId} found.");
            }

            return result.Value;
        }

        public IEnumerable<Story> GetStories()
        {
            return _stories.Values;
        }

        public Story EditStory(Story story)
        {
            var result = _stories.FirstOrDefault(s => s.Key == story.Id);
            if (result.Value is null)
            {
                throw new RepositoryException($"No story with ID {story.Id} found.");
            }

            _stories[result.Key].Title = story.Title;
            _stories[result.Key].Scenes = story.Scenes;
            _stories[result.Key].StartingSceneId = story.StartingSceneId;
            _stories[result.Key].Id = story.Id;

            return result.Value;
        }
    }
}
