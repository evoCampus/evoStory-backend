using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class StoryService(IStoryRepository storyRepository,IDTOConversionService dTOConversion) : IStoryService
    {
        public void CreateStory(CreateStoryDTO story)
        {
            var newStoryId = Guid.NewGuid();
            var newSceneId = Guid.NewGuid();
            var newStory = new Story
            {
                Id = newStoryId,
                Scenes = story.Scenes.Select(sceneDTO => new Scene()
                {
                    Choices = sceneDTO.Choices.Select(choiceDTO => new Choice()
                    {
                        ChoiceText = choiceDTO.ChoiceText,
                        Id = Guid.NewGuid(),
                        NextSceneId = choiceDTO.NextSceneId,
                        SceneId = newSceneId
                    }),
                    Content = new Content
                    {
                        Id = Guid.NewGuid(),
                        Text = sceneDTO.Content.Text,
                        ImageId = sceneDTO.Content.ImageId,
                        SoundId = sceneDTO.Content.SoundId
                    },
                    Id = newSceneId,
                    StoryId= newStoryId
                }),
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
            };
            storyRepository.CreateStory(newStory);
        }

        public void DeleteStory(Guid storyId)
        {
            storyRepository.DeleteStory(storyId);
        }

        public StoryDTO GetStory(Guid storyId)
        {
            var result = storyRepository.GetStory(storyId);
            if (result == null)
            {
                return null;
            }
            var storyDTO = dTOConversion.ConvertStoryToStoryDTO(result);
            return storyDTO;
        }

        public IEnumerable<StoryDTO> GetStories()
        {
            var result = storyRepository.GetStories();
            if (result == null)
            {
                return null;
            }
            var storiesDTO = result.Select(story => dTOConversion.ConvertStoryToStoryDTO(story));
            return storiesDTO;
        }

        public void EditStory(EditStoryDTO story)
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
        }
    }
}
