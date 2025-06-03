using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory(IDatabase dbContext, ILogger<SceneRepositoryInMemory> logger) : ISceneRepository
    {
        public Scene CreateScene(Scene scene)
        {
            logger.LogTrace("Create scene repository was called.");
            dbContext.AddScene(scene);
            return scene;
        }

        public Scene DeleteScene(Guid sceneId)
        {
            logger.LogTrace("Delete scene repository was called.");
            var result = dbContext.RemoveScene(sceneId);
            return result;
        }

        public Scene? GetScene(Guid sceneId)
        {
            logger.LogTrace("Get scene repository was called.");
            var result = dbContext.GetScene(sceneId);
            return result;
        }

        public IEnumerable<Scene> GetScenes()
        {
            logger.LogTrace("Get scenes repository was called.");
            return dbContext.GetAllScenes();
        }
    }
}
