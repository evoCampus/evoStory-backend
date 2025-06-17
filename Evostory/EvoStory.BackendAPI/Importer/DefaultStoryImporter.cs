using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;
using Evostory.Story.Models;
using System.Text.Json;

namespace EvoStory.BackendAPI.Importer
{
    public class DefaultStoryImporter(IStoryRepository storyRepository) : IDefaultStoryImporter
    {
        public void ImportStory()
        {
            string storyFile = File.ReadAllText(@"Resource\FadingMemories.json");
            if (string.IsNullOrWhiteSpace(storyFile))
            {
                throw new FileNotFoundException("The story file is empty or not found.");
            }
            var story = JsonSerializer.Deserialize<ImportStoryDTO>(storyFile);
            Guid[] sceneIds = new Guid[story.Scenes.Count() + 1];
            for (int i = 1; i < story.Scenes.Count(); i++)
            {
                sceneIds[i] = new Guid();
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
                        Id = new Guid(),
                        ChoiceText = c.ChoiceText,
                        NextSceneId = sceneIds[c.NextSceneId]
                    }).ToList()
                }).ToList(),
                StartingSceneId = sceneIds[story.StartingSceneId]
            };
            storyRepository.CreateStory(storyModel);
        }
    }
}
