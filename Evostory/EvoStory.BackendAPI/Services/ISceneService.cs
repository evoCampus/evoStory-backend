using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface ISceneService
    {
        public SceneDTO CreateScene(CreateSceneDTO scene);
        public SceneDTO GetScene(Guid sceneId);
        public IEnumerable<SceneDTO> GetScenes();
        public SceneDTO DeleteScene(Guid sceneId);
    }
}
