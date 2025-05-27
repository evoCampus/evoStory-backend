using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class StoryService(IStoryRepository storyRepository, IDTOConversionService dTOConversion, ILogger<StoryService> logger) : IStoryService
    {
        public void CreateStory(CreateStoryDTO story)
        {
            logger.LogDebug($"Create story service was called where Title: {story.Title};");
            var newStory = new Story
            {
                Id = Guid.NewGuid(),
                Scenes = story.Scenes.Select(sceneDTO => new Scene()
                {
                    Choices = sceneDTO.Choices.Select(choiceDTO => new Choice()
                    {
                        ChoiceText = choiceDTO.ChoiceText,
                        Id = Guid.NewGuid(),
                        NextSceneId = choiceDTO.NextSceneId
                    }).ToList(),
                    Content = new Content
                    {
                        Id = Guid.NewGuid(),
                        Text = sceneDTO.Content.Text,
                        ImageId = sceneDTO.Content.ImageId,
                        SoundId = sceneDTO.Content.SoundId
                    },
                    Id = Guid.NewGuid()
                }),
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
            };
            logger.LogInformation($"Story was created successfully with Id: {newStory.Id}");
            storyRepository.CreateStory(newStory);
        }

        public void DeleteStory(Guid storyId)
        {
            logger.LogDebug("Delete story service was called.");
            storyRepository.DeleteStory(storyId);
            logger.LogInformation($"Story with Id: {storyId} was deleted.");
        }

        public StoryDTO GetStory(Guid storyId)
        {
            logger.LogDebug($"Get story service was called with Id: {storyId};");
            var result = storyRepository.GetStory(storyId);
            if (result == null)
            {
                logger.LogWarning($"Story with Id: {storyId} was not found.");
                return null;
            }
            logger.LogDebug($"Story with Id: {storyId} was found.");
            var storyDTO = dTOConversion.ConvertStoryToStoryDTO(result);
            return storyDTO;
        }

        public IEnumerable<StoryDTO> GetStories()
        {
            logger.LogDebug("Get stories service was called.");
            var result = storyRepository.GetStories();
            if (result == null)
            {
                logger.LogWarning("No stories were found.");
                return null;
            }
            logger.LogDebug("Stories were found.");
            var storiesDTO = result.Select(story => dTOConversion.ConvertStoryToStoryDTO(story));
            return storiesDTO;
        }

        public void EditStory(EditStoryDTO story)
        {
            logger.LogDebug($"Edit story service was called where Id: {story.Id};");
            var newStory = new Story
            {
                Id = story.Id,
                Scenes = story.Scenes.Select(sceneDTO => new Scene()
                {
                    Choices = sceneDTO.Choices.Select(choiceDTO => new Choice()
                    {
                        ChoiceText = choiceDTO.ChoiceText,
                        Id = choiceDTO.Id,
                        NextSceneId = choiceDTO.NextSceneId
                    }).ToList(),
                    Content = new Content
                    {
                        Id = sceneDTO.Content.Id,
                        Text = sceneDTO.Content.Text,
                        ImageId = sceneDTO.Content.ImageId,
                        SoundId = sceneDTO.Content.SoundId
                    },
                    Id = sceneDTO.Id
                }),
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
            };
            logger.LogInformation($"Story with Id: {newStory.Id} was edited successfully.");
            storyRepository.EditStory(newStory);
        }
    }
}
