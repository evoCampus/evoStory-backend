using EvoStory.BackendAPI.Repository;
using EvoStory.Database.Models;
using System.Text.Json;

namespace EvoStory.BackendAPI.Importer
{
    public class DefaultStoryImporter(IStoryRepository storyRepository, ILogger<DefaultStoryImporter> logger) : IStoryImporter
    {
        public void ImportStory()
        {
            logger.LogInformation("Importing default story from FadingMemories.json file.");
            var path = Path.Combine("Resource", "FadingMemories.json");
            string storyFile = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(storyFile))
            {
                logger.LogError("The story file is empty or not found.");
                throw new FileNotFoundException("The story file is empty or not found.");
            }
            var story = JsonSerializer.Deserialize<ImportStoryModel>(storyFile);
            var sceneIds = new Dictionary<int, Guid>(story.Scenes.Count());
            foreach(var scene in story.Scenes)
            {
                sceneIds.Add(scene.SceneId, Guid.NewGuid());
            }
            var storyModel = new Story
            {
                Id = Guid.NewGuid(),
                Title = story.Title,
                Scenes = story.Scenes.Select(s => new Scene
                {
                    Id = sceneIds[s.SceneId],
                    Content = new Content
                    {
                        Id = Guid.NewGuid(),
                        Text = s.Content.Text,
                        ImageId = Guid.Empty,
                        SoundId = Guid.Empty
                    },
                    Choices = s.Choices == null ? new List<Choice>() : s.Choices.Select(c => new Choice
                    {
                        Id = Guid.NewGuid(),
                        ChoiceText = c.ChoiceText,
                        NextSceneId = sceneIds[c.NextSceneId]
                    }).ToList()
                }).ToList(),
                StartingSceneId = sceneIds[story.StartingSceneId]
            };
            storyRepository.CreateStory(storyModel);
            logger.LogInformation($"Default story '{story.Title}' imported successfully with ID: {storyModel.Id}.");
        }
    }
}
