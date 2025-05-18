using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory(ILogger<SceneRepositoryInMemory> logger) : ISceneRepository
    {
        private Dictionary<Guid, Scene> scenes = new();
        public Scene CreateScene(Scene scene)
        {
            logger.LogInformation("Create scene repository was called.");
            scenes.Add(scene.Id, scene);
            return scene;
        }

        public void DeleteScene(Guid sceneId)
        {
            logger.LogInformation("Delete scene repository was called.");
            var result = scenes.FirstOrDefault(scene => scene.Key == sceneId);
            scenes.Remove(result.Key);
        }

        public Scene? GetScene(Guid sceneId)
        {
            logger.LogInformation("Get scene repository was called.");
            var result = scenes.FirstOrDefault(scene => scene.Key == sceneId);
            return result.Value;
        }

        public IEnumerable<Scene> GetScenes()
        {
            logger.LogInformation("Get scenes repository was called.");
            return scenes.Values;
        }
    }
}
