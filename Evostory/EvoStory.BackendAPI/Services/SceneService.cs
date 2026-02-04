using EvoStory.Database.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.Database.Repository;
using System.Threading.Tasks;
using System.Linq;

namespace EvoStory.BackendAPI.Services
{
    public class SceneService : ISceneService
    {
        private readonly ISceneRepository _sceneRepository;
        private readonly IDTOConversionService _dTOConversion;
        private readonly ILogger<SceneService> _logger;
        public SceneService(ISceneRepository sceneRepository, IDTOConversionService dtoConversion, ILogger<SceneService> logger)
        {
            _sceneRepository = sceneRepository;
            _dTOConversion = dtoConversion;
            _logger = logger;
        }
        public async Task<SceneDTO> CreateScene(CreateSceneDTO scene)
        {
            _logger.LogDebug("Create scene service was called.");
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
                    NextSceneId = null
              }).ToList()
            };
            var createdScene = await _sceneRepository.CreateScene(newScene, Guid.Empty);
            _logger.LogInformation($"Scene was created successfully with Id: {newScene.Id}");
            return _dTOConversion.ConvertSceneToSceneDTO(createdScene);
        }

        public async Task<SceneDTO> DeleteScene(Guid sceneId)
        {
            _logger.LogDebug("Delete scene service was called.");
            var result = await _sceneRepository.DeleteScene(sceneId);
            _logger.LogInformation($"Scene with Id: {sceneId} was deleted.");
            return _dTOConversion.ConvertSceneToSceneDTO(result);
        }

        public async Task<SceneDTO> GetScene(Guid sceneId)
        {
            _logger.LogDebug("Get scene service was called.");

            var result = await _sceneRepository.GetScene(sceneId);

            if (sceneId == null)
            {
                throw new KeyNotFoundException($"Scene with ID {sceneId} not found.");
            }

            return _dTOConversion.ConvertSceneToSceneDTO(result);
        }

        public async Task<IEnumerable<SceneDTO>> GetScenes()
        {
            _logger.LogDebug("Get scenes service was called.");
            var result = await _sceneRepository.GetScenes();
            var scenesDTO = result.Select(scene => _dTOConversion.ConvertSceneToSceneDTO(scene));
            return scenesDTO;
        }
    }
}
