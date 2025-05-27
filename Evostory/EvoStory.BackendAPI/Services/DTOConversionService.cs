using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public class DTOConversionService : IDTOConversionService
    {
        public StoryDTO ConvertStoryToStoryDTO(Story story)
        {
            return new StoryDTO
            {
                Id = story.Id,
                Title = story.Title,
                StartingSceneId = story.StartingSceneId,
            };
        }
        public SceneDTO ConvertSceneToSceneDTO(Scene scene)
        {
            return new SceneDTO
            {
                Id = scene.Id,
                Content = new ContentDTO
                {
                    Id = scene.Content.Id,
                    Text = scene.Content.Text,
                    ImageId = scene.Content.ImageId,
                    SoundId = scene.Content.SoundId
                },
                Choices = scene.Choices.Select(c => ConvertChoiceToChoiceDTO(c))
            };
        }
        public ChoiceDTO ConvertChoiceToChoiceDTO(Choice choice)
        {
            return new ChoiceDTO
            {
                Id = choice.Id,
                ChoiceText = choice.ChoiceText,
                NextSceneId = choice.NextSceneId
            };
        }
    }
}
