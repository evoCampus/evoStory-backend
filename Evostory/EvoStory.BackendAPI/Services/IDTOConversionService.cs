using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IDTOConversionService
    {
        public StoryDTO ConvertStoryToStoryDTO(Story story);
        public SceneDTO ConvertSceneToSceneDTO(Scene scene);
        public ChoiceDTO ConvertChoiceToChoiceDTO(Choice choice);
        public UserDTO ConvertUserToUserDTO(User user);

    }
}
