using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class SceneService(ISceneRepository sceneRepository) : ISceneService
    {
        public void CreateScene(CreateSceneDTO scene)
        {
            var newScene = new Scene
            {
                Id = Guid.NewGuid(),
                Content = new Content
                {
                    Text = scene.Content.Text,
                    ImageId = scene.Content.ImageId,
                    SoundId = scene.Content.SoundId
                },
                Choices = scene.Choices.Select(choiceDTO => new Choice()
                {
                    ChoiceText = choiceDTO.ChoiceText,
                    Id = Guid.NewGuid(),
                    NextSceneId = choiceDTO.NextSceneId
                }).ToList()
            };
            sceneRepository.CreateScene(newScene);
        }

        public void DeleteScene(Guid sceneId)
        {
            try
            {
                sceneRepository.DeleteScene(sceneId);
            }
            catch(Exception ex)
            {
                throw new Exception("Not Found", ex);
            }
        }

        public SceneDTO? GetScene(Guid sceneId)
        {
            var result = sceneRepository.GetScene(sceneId);
            if (result == null)
            {
                return null;
            }
            var sceneDTO = new SceneDTO
            {
                Id = result.Id,
                Content = new ContentDTO
                {
                    Text = result.Content.Text,
                    ImageId = result.Content.ImageId,
                    SoundId = result.Content.SoundId
                },
                Choices = result.Choices.Select(choice => new ChoiceDTO
                {
                    ChoiceText = choice.ChoiceText,
                    Id = choice.Id,
                    NextSceneId = choice.NextSceneId
                }).ToList()
            };
            return sceneDTO;
        }

        public IEnumerable<SceneDTO>? GetScenes()
        {
            var result = sceneRepository.GetScenes();
            if (result == null)
            {
                return null;
            }
            var scenesDTO = result.Select(scene => new SceneDTO
            {
                Id = scene.Id,
                Content = new ContentDTO
                {
                    Text = scene.Content.Text,
                    ImageId = scene.Content.ImageId,
                    SoundId = scene.Content.SoundId
                },
                Choices = scene.Choices.Select(choice => new ChoiceDTO
                {
                    ChoiceText = choice.ChoiceText,
                    Id = choice.Id,
                    NextSceneId = choice.NextSceneId
                }).ToList()
            });
            return scenesDTO;
        }
    }
}
