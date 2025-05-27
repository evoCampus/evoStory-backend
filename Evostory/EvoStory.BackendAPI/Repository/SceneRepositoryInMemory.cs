using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory(ILogger<SceneRepositoryInMemory> logger) : ISceneRepository
    {
        private Dictionary<Guid, Scene> scenes = new();
        public Scene CreateScene(Scene scene)
        {
            logger.LogTrace("Create scene repository was called.");
            scenes.Add(scene.Id, scene);
            return scene;
        }

        public void DeleteScene(Guid sceneId)
        {
            logger.LogTrace("Delete scene repository was called.");
            var result = scenes.FirstOrDefault(scene => scene.Key == sceneId);
            scenes.Remove(result.Key);
        }

        public Scene? GetScene(Guid sceneId)
        {
            logger.LogTrace("Get scene repository was called.");
            var result = scenes.FirstOrDefault(scene => scene.Key == sceneId);
            return result.Value;
        }

        public IEnumerable<Scene> GetScenes()
        {
            logger.LogTrace("Get scenes repository was called.");
            return scenes.Values;
        }
    }
}
