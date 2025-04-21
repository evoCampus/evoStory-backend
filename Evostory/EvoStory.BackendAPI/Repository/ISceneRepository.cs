using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface ISceneRepository
    {
        public void CreateScene(Scene scene);
        public Scene? GetScene(Guid sceneId);
        public IEnumerable<Scene>? GetScenes();
        public void DeleteScene(Guid sceneId);
    }
}
