using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory(IDatabase dbContext) : ISceneRepository
    {
        public Scene CreateScene(Scene scene)
        {
            dbContext.AddScene(scene);
            return scene;
        }

        public Scene DeleteScene(Guid sceneId)
        {
            var result = dbContext.RemoveScene(sceneId);
            return result;
        }

        public Scene? GetScene(Guid sceneId)
        {
            var result = dbContext.GetScene(sceneId);
            return result;
        }

        public IEnumerable<Scene> GetScenes()
        {
            return dbContext.GetAllScenes();
        }
    }
}
