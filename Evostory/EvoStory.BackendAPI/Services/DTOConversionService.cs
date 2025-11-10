using EvoStory.Database.Models;
using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public class DTOConversionService(ILogger<DTOConversionService> logger) : IDTOConversionService
    {
        public StoryDTO ConvertStoryToStoryDTO(Story story)
        {
            logger.LogTrace($"Converting Story with Id: {story.Id} to StoryDTO.");
            return new StoryDTO
            {
                Id = story.Id,
                Title = story.Title,
                StartingSceneId = story.StartingSceneId,
            };
        }
        public SceneDTO ConvertSceneToSceneDTO(Scene scene)
        {
            logger.LogTrace($"Converting Scene with Id: {scene.Id} to SceneDTO.");
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
            logger.LogTrace($"Converting Choice with Id: {choice.Id} to ChoiceDTO.");
            return new ChoiceDTO
            {
                Id = choice.Id,
                ChoiceText = choice.ChoiceText,
                NextSceneId = choice.NextSceneId
            };
        }

        public UserDTO ConvertUserToUserDTO(User user)
        {
            logger.LogTrace($"Converting User with Id: {user.Id} to UserDTO.");
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
