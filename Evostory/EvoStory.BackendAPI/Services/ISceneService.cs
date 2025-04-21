using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface ISceneService
    {
        public void CreateScene(CreateSceneDTO scene);
        public SceneDTO? GetScene(Guid sceneId);
        public IEnumerable<SceneDTO>? GetScenes();
        public void DeleteScene(Guid sceneId);
    }
}
