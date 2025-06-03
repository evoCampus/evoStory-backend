using Evostory.Story.Models;
namespace EvoStory.BackendAPI.Database
{
    public class DatabaseInMemmory : IDatabase
    {
        public Dictionary<Guid, Story> Stories { get; } = new();
        public void AddStory(Story story)
        {
            Stories.Add(story.Id, story);
        }
        public Story RemoveStory(Guid storyId)
        {
            var result = GetStory(storyId);
            if (result != null)
            {
                Stories.Remove(storyId);
                return result;
            }
            throw new KeyNotFoundException($"No story with ID {storyId} found.");
        }
        public Story GetStory(Guid storyId)
        {
            var result = Stories.FirstOrDefault(s => s.Key == storyId).Value;
            if (result != null)
            {
                return result;
            }
            throw new KeyNotFoundException($"No story with ID {storyId} found.");
        }
        public IEnumerable<Story> GetAllStories()
        {
            return Stories.Values;
        }
        public Story UpdateStory(Story story)
        {
            if (Stories.TryGetValue(story.Id, out var existingStory))
            {
                existingStory.Title = story.Title;
                existingStory.Scenes = story.Scenes;
                existingStory.StartingSceneId = story.StartingSceneId;
                return existingStory;
            }
            throw new KeyNotFoundException($"No story with ID {story.Id} found.");
        }
        public void AddScene(Scene scene)
        {
            if (Stories.TryGetValue(scene.StoryId, out var story))
            {
                story.Scenes.Append(scene);
            }
            else
            {
                throw new KeyNotFoundException($"No story with ID {scene.StoryId} found.");
            }
        }
        public Scene RemoveScene(Guid sceneId)
        {
            var story = Stories.Values.FirstOrDefault(s => s.Scenes.Any(sc => sc.Id == sceneId));
            if (story != null)
            {
                var scene = story.Scenes.FirstOrDefault(s => s.Id == sceneId);
                story.Scenes = story.Scenes.Where(s => s.Id != sceneId);
                return scene;
            }
            else
            {
                throw new KeyNotFoundException($"No scene with ID {sceneId} found.");
            }
        }
        public Scene GetScene(Guid sceneId)
        {
            foreach (var story in Stories.Values)
            {
                var scene = story.Scenes.FirstOrDefault(s => s.Id == sceneId);
                if (scene != null)
                {
                    return scene;
                }
            }
            throw new KeyNotFoundException($"No scene with ID {sceneId} found.");
        }
        public IEnumerable<Scene> GetAllScenes()
        {
            return Stories.Values.SelectMany(story => story.Scenes);
        }
        public void AddChoice(Choice choice)
        {
            var scene = GetScene(choice.SceneId);
            if (scene != null)
            {
                scene.Choices.Append(choice);
                var story = Stories.Values.FirstOrDefault(s => s.Scenes.Any(sc => sc.Id == choice.SceneId));
                story.Scenes = story.Scenes.Select(s => s.Id == choice.SceneId ? scene : s);
                UpdateStory(story);
            }
            else
            {
                throw new KeyNotFoundException($"No scene with ID {choice.SceneId} found.");
            }
        }
        public Choice RemoveChoice(Guid choiceId)
        {
            var scene = Stories.Values.FirstOrDefault(s => s.Scenes.Any(sc => sc.Choices.Any(c => c.Id == choiceId))).Scenes.FirstOrDefault(sc => sc.Choices.Any(c => c.Id == choiceId));
            if (scene != null)
            {
                var choice = scene.Choices.FirstOrDefault(c => c.Id == choiceId);
                scene.Choices = scene.Choices.Where(c => c.Id != choiceId);
                return choice;
            }
            else
            {
                throw new KeyNotFoundException($"No scene contains any choice with ID {choiceId}.");
            }
        }
        public Choice GetChoice(Guid choiceId)
        {
            foreach (var story in Stories.Values)
            {
                foreach (var scene in story.Scenes)
                {
                    var choice = scene.Choices.FirstOrDefault(c => c.Id == choiceId);
                    if (choice != null)
                    {
                        return choice;
                    }
                }
            }
            throw new KeyNotFoundException($"No choice with ID {choiceId} found.");
        }
        public IEnumerable<Choice> GetAllChoices()
        {
            return Stories.Values.SelectMany(story => story.Scenes.SelectMany(scene => scene.Choices));
        }
    }
}