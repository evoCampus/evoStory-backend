using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class SceneService(ISceneRepository sceneRepository) : ISceneService
    {
        public SceneDTO? CreateScene(CreateSceneDTO scene)
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
            return new SceneDTO
            {
                Id = newScene.Id,
                Content = new ContentDTO
                {
                    Id = newScene.Content.Id,
                    Text = newScene.Content.Text,
                    ImageId = newScene.Content.ImageId,
                    SoundId = newScene.Content.SoundId
                },
                Choices = newScene.Choices.Select(choice => new ChoiceDTO
                {
                    ChoiceText = choice.ChoiceText,
                    Id = choice.Id,
                    NextSceneId = choice.NextSceneId
                })
            };
        }

        public void DeleteScene(Guid sceneId)
        {
            sceneRepository.DeleteScene(sceneId);
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
                    Id = result.Content.Id,
                    Text = result.Content.Text,
                    ImageId = result.Content.ImageId,
                    SoundId = result.Content.SoundId
                },
                Choices = result.Choices.Select(choice => new ChoiceDTO
                {
                    ChoiceText = choice.ChoiceText,
                    Id = choice.Id,
                    NextSceneId = choice.NextSceneId
                })
            };
            return sceneDTO;
        }

        public IEnumerable<SceneDTO> GetScenes()
        {
            var result = sceneRepository.GetScenes();
            var scenesDTO = result.Select(scene => new SceneDTO
            {
                Id = scene.Id,
                Content = new ContentDTO
                {
                    Id = scene.Content.Id,
                    Text = scene.Content.Text,
                    ImageId = scene.Content.ImageId,
                    SoundId = scene.Content.SoundId
                },
                Choices = scene.Choices.Select(choice => new ChoiceDTO
                {
                    ChoiceText = choice.ChoiceText,
                    Id = choice.Id,
                    NextSceneId = choice.NextSceneId
                })
            });
            return scenesDTO;
        }
    }
}
