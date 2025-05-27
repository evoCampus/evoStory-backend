using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class StoryService(IStoryRepository storyRepository, IDTOConversionService dTOConversion) : IStoryService
    {
        public StoryDTO CreateStory(CreateStoryDTO story)
        {
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
            storyRepository.CreateStory(newStory);
            return dTOConversion.ConvertStoryToStoryDTO(newStory);
        }

        public StoryDTO DeleteStory(Guid storyId)
        {
            var result = storyRepository.DeleteStory(storyId);
            return dTOConversion.ConvertStoryToStoryDTO(result);
        }

        public StoryDTO GetStory(Guid storyId)
        {
            var result = storyRepository.GetStory(storyId);
            return dTOConversion.ConvertStoryToStoryDTO(result);
        }

        public IEnumerable<StoryDTO> GetStories()
        {
            var result = storyRepository.GetStories();
            var storiesDTO = result.Select(story => dTOConversion.ConvertStoryToStoryDTO(story));
            return storiesDTO;
        }

        public StoryDTO EditStory(EditStoryDTO story)
        {
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
            storyRepository.EditStory(newStory);
            return dTOConversion.ConvertStoryToStoryDTO(newStory);
        }
    }
}
