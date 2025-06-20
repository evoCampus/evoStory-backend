using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IDTOConversionService
    {
        StoryDTO ConvertStoryToStoryDTO(Story story);
        SceneDTO ConvertSceneToSceneDTO(Scene scene);
        ChoiceDTO ConvertChoiceToChoiceDTO(Choice choice);
        UserDTO ConvertUserToUserDTO(User user);

    }
}
