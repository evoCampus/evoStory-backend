using EvoStory.Database.Repository;
using EvoStory.Database.Models;
using System.Text.Json;

namespace EvoStory.BackendAPI.Importer
{
    public class DefaultStoryImporter(IStoryRepository storyRepository, ILogger<DefaultStoryImporter> logger) : IStoryImporter
    {
        public async Task ImportStory()
        {
            var resourceDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource");
            logger.LogInformation($"Searching for stories at: {resourceDirectory}");

            if (!Directory.Exists(resourceDirectory))
            {
                logger.LogWarning($"The 'Resource' directory was not found at: {resourceDirectory}. Creating it...");
                Directory.CreateDirectory(resourceDirectory);
                return;
            }

            var storyFiles = Directory.GetFiles(resourceDirectory, "*.json");

            if (storyFiles.Length == 0)
            {
                logger.LogWarning("No .json files found in the Resource directory.");
                return;
            }

            logger.LogInformation($"Found {storyFiles.Length} story file(s). Starting processing...");

            foreach (var filePath in storyFiles)
            {
                await ProcessStoryFile(filePath);
            }
        }

        private async Task ProcessStoryFile(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            try
            {
                string jsonContent = await File.ReadAllTextAsync(filePath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    logger.LogWarning($"The file '{fileName}' is empty, skipping.");
                    return;
                }

                var storyDto = JsonSerializer.Deserialize<ImportStoryModel>(jsonContent);

                if (storyDto == null || string.IsNullOrEmpty(storyDto.Title))
                {
                    logger.LogWarning($"The file '{fileName}' does not contain a valid story format.");
                    return;
                }

                var existingStories = await storyRepository.GetStories();
                if (existingStories.Any(s => s.Title == storyDto.Title))
                {
                    logger.LogInformation($"The story '{storyDto.Title}' already exists in the database. ({fileName}) -> Skipped.");
                    return;
                }

                var newStoryId = Guid.NewGuid();
                var itemMap = new Dictionary<string, Guid>();
                var itemsToSave = new List<Item>();

                if (storyDto.Items != null)
                {
                    foreach (var item in storyDto.Items)
                    {
                        var newItem = new Item
                        {
                            Id = Guid.NewGuid(),
                            Name = item.Name,
                            Description = item.Description,
                            StoryId = newStoryId,
                            IsStackable = false
                        };
                        itemsToSave.Add(newItem);
                        itemMap[item.Name] = newItem.Id;
                    }
                }

                var sceneIds = new Dictionary<int, Guid>();
                if (storyDto.Scenes != null)
                {
                    foreach (var scene in storyDto.Scenes)
                    {
                        sceneIds[scene.SceneId] = Guid.NewGuid();
                    }
                }
                
                var storyModel = new Story
                {
                    Id = newStoryId,
                    Title = storyDto.Title,
                    Items = itemsToSave,
                    Scenes = storyDto.Scenes?.Select(s => new Scene
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
                            NextSceneId = sceneIds.ContainsKey(c.NextSceneId) ? sceneIds[c.NextSceneId] : Guid.Empty,

                            RewardItemId = (!string.IsNullOrEmpty(c.RewardItemName) && itemMap.ContainsKey(c.RewardItemName))
                                           ? itemMap[c.RewardItemName] : null,

                            RequiredItemId = (!string.IsNullOrEmpty(c.RequiredItemName) && itemMap.ContainsKey(c.RequiredItemName))
                                             ? itemMap[c.RequiredItemName] : null
                        }).ToList()
                    }).ToList() ?? new List<Scene>(),
                    StartingSceneId = sceneIds.ContainsKey(storyDto.StartingSceneId) ? sceneIds[storyDto.StartingSceneId] : Guid.Empty
                };

                await storyRepository.CreateStory(storyModel);
                logger.LogInformation($"SUCCESS: '{storyDto.Title}' imported from: {fileName}");
            }
            catch (Exception ex)
            {
                logger.LogError($"ERROR processing file '{fileName}': {ex.Message}");
            }
        }
    }
}