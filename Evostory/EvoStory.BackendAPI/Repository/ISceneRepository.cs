using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface ISceneRepository
    {
        Scene? CreateScene(Scene scene, Guid storyId);
        Scene? GetScene(Guid sceneId);
        IEnumerable<Scene> GetScenes();
        Scene DeleteScene(Guid sceneId);
    }
}
