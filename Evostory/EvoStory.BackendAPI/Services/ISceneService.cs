using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface ISceneService
    {
        SceneDTO? CreateScene(CreateSceneDTO scene);
        SceneDTO? GetScene(Guid sceneId);
        IEnumerable<SceneDTO> GetScenes();
        void DeleteScene(Guid sceneId);
    }
}
