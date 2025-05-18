using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class SceneService(ISceneRepository sceneRepository, IDTOConversionService dTOConversion, ILogger<SceneService> logger) : ISceneService
    {
        public SceneDTO? CreateScene(CreateSceneDTO scene)
        {
            logger.LogInformation("Create scene service was called.");
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
            logger.LogDebug($"Scene was created successfully with Id: {newScene.Id}");
            sceneRepository.CreateScene(newScene);
            return dTOConversion.ConvertSceneToSceneDTO(newScene);
        }

        public void DeleteScene(Guid sceneId)
        {
            logger.LogInformation("Delete scene service was called.");
            sceneRepository.DeleteScene(sceneId);
            logger.LogDebug($"Scene with Id: {sceneId} was deleted.");
        }

        public SceneDTO? GetScene(Guid sceneId)
        {
            logger.LogInformation("Get scene service was called.");
            var result = sceneRepository.GetScene(sceneId);
            if (result == null)
            {
                logger.LogWarning($"Scene with Id: {sceneId} was not found.");
                return null;
            }
            logger.LogDebug($"Scene with Id: {sceneId} was found.");
            var sceneDTO = dTOConversion.ConvertSceneToSceneDTO(result);
            return sceneDTO;
        }

        public IEnumerable<SceneDTO> GetScenes()
        {
            logger.LogInformation("Get scenes service was called.");
            var result = sceneRepository.GetScenes();
            var scenesDTO = result.Select(scene => dTOConversion.ConvertSceneToSceneDTO(scene));
            logger.LogDebug("Scenes were found.");
            return scenesDTO;
        }
    }
}
