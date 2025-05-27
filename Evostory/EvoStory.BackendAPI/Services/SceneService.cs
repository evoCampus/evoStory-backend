using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class SceneService(ISceneRepository sceneRepository, IDTOConversionService dTOConversion) : ISceneService
    {
        public SceneDTO CreateScene(CreateSceneDTO scene)
        {
            var newScene = new Scene
            {
                Id = Guid.NewGuid(),
                Content = new Content
                {
                    Id = Guid.NewGuid(),
                    Text = scene.Content.Text,
                    ImageId = scene.Content.ImageId,
                    SoundId = scene.Content.SoundId
                },
                Choices = scene.Choices.Select(choiceDTO => new Choice
                {
                    ChoiceText = choiceDTO.ChoiceText,
                    Id = Guid.NewGuid(),
                    NextSceneId = choiceDTO.NextSceneId
                })
            };
            sceneRepository.CreateScene(newScene);
            return dTOConversion.ConvertSceneToSceneDTO(newScene);
        }

        public SceneDTO DeleteScene(Guid sceneId)
        {
            var result = sceneRepository.DeleteScene(sceneId);
            return dTOConversion.ConvertSceneToSceneDTO(result);
        }

        public SceneDTO GetScene(Guid sceneId)
        {
            var result = sceneRepository.GetScene(sceneId);
            return dTOConversion.ConvertSceneToSceneDTO(result);
        }

        public IEnumerable<SceneDTO> GetScenes()
        {
            var result = sceneRepository.GetScenes();
            var scenesDTO = result.Select(scene => dTOConversion.ConvertSceneToSceneDTO(scene));
            return scenesDTO;
        }
    }
}
