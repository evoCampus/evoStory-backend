using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface ISceneRepository
    {
        public Scene? CreateScene(Scene scene);
        public Scene? GetScene(Guid sceneId);
        public IEnumerable<Scene> GetScenes();
        public Scene DeleteScene(Guid sceneId);
    }
}
