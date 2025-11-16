using EvoStory.Database.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.Database.Repository;
using System.Threading.Tasks;

namespace EvoStory.BackendAPI.Services
{
    public class StoryService(IStoryRepository storyRepository, IDTOConversionService dTOConversion, ILogger<StoryService> logger) : IStoryService
    {
        public async Task<StoryDTO> CreateStory(CreateStoryDTO story)
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
                }).ToList(),
                StartingSceneId = story.StartingSceneId ?? Guid.Empty,
                Title = story.Title
            };
            
            logger.LogInformation($"Story was created successfully with Id: {newStory.Id}");
            var createdStory = await storyRepository.CreateStory(newStory);
            return dTOConversion.ConvertStoryToStoryDTO(createdStory);
        }

        public async Task<StoryDTO> DeleteStory(Guid storyId)
        {
            logger.LogDebug("Delete story service was called.");
            var result = await storyRepository.DeleteStory(storyId);
            logger.LogInformation($"Story with Id: {storyId} was deleted.");
            return dTOConversion.ConvertStoryToStoryDTO(result);
        }

        public async Task<StoryDTO> GetStory(Guid storyId)
        {
            logger.LogDebug($"Get story service was called with Id: {storyId};");
            var result = await storyRepository.GetStory(storyId);
            return dTOConversion.ConvertStoryToStoryDTO(result);
        }

        public async Task<IEnumerable<StoryDTO>> GetStories()
        {
            logger.LogDebug("Get stories service was called.");
            var result = await storyRepository.GetStories();
            var storiesDTO = result.Select(story => dTOConversion.ConvertStoryToStoryDTO(story));
            return storiesDTO;
        }

        public async Task<StoryDTO> EditStory(EditStoryDTO story)
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
                }).ToList(),
                StartingSceneId = story.StartingSceneId ?? Guid.Empty,
                Title = story.Title
            };
            
            logger.LogInformation($"Story with Id: {newStory.Id} was edited successfully.");
            var editedStory = await storyRepository.EditStory(newStory);
            return dTOConversion.ConvertStoryToStoryDTO(editedStory);
        }
    }
}
