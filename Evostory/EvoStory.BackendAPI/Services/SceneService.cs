using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class SceneService(ISceneRepository sceneRepository, IDTOConversionService dTOConversion, ILogger<SceneService> logger) : ISceneService
    {
        public SceneDTO CreateScene(CreateSceneDTO scene)
        {
            logger.LogDebug("Create scene service was called.");
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
            logger.LogInformation($"Scene was created successfully with Id: {newScene.Id}");
            sceneRepository.CreateScene(newScene);
            return dTOConversion.ConvertSceneToSceneDTO(newScene);
        }

        public SceneDTO DeleteScene(Guid sceneId)
        {
            logger.LogDebug("Delete scene service was called.");
            var result = sceneRepository.DeleteScene(sceneId);
            logger.LogInformation($"Scene with Id: {sceneId} was deleted.");
            return dTOConversion.ConvertSceneToSceneDTO(result);
        }

        public SceneDTO GetScene(Guid sceneId)
        {
            logger.LogDebug("Get scene service was called.");
            var result = sceneRepository.GetScene(sceneId);
            return dTOConversion.ConvertSceneToSceneDTO(result);
        }

        public IEnumerable<SceneDTO> GetScenes()
        {
            logger.LogDebug("Get scenes service was called.");
            var result = sceneRepository.GetScenes();
            var scenesDTO = result.Select(scene => dTOConversion.ConvertSceneToSceneDTO(scene));
            return scenesDTO;
        }
    }
}
