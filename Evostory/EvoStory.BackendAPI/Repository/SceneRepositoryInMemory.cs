using Evostory.Story.Models;
using EvoStory.BackendAPI.Exceptions;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory(ILogger<SceneRepositoryInMemory> logger) : ISceneRepository
    {
        private Dictionary<Guid, Scene> _scenes = new();
        public Scene CreateScene(Scene scene)
        {
            logger.LogTrace("Create scene repository was called.");
            if (_scenes.ContainsKey(scene.Id))
            {
                throw new RepositoryException($"Existing scene with {scene.Id} found.");
            }
            
            _scenes.Add(scene.Id, scene);
            return scene;
        }

        public Scene DeleteScene(Guid sceneId)
        {
            logger.LogTrace("Delete scene repository was called.");
            var result = _scenes.FirstOrDefault(scene => scene.Key == sceneId);
            if (result.Value is null)
            {
                throw new RepositoryException($"No scene with {sceneId} found.");
            }

            _scenes.Remove(result.Key);
            return result.Value;
        }

        public Scene GetScene(Guid sceneId)
        {
            logger.LogTrace("Get scene repository was called.");
            var result = _scenes.FirstOrDefault(scene => scene.Key == sceneId);
            if (result.Value is null)
            {
                throw new RepositoryException($"No scene with {sceneId} found.");
            }

            return result.Value;
        }

        public IEnumerable<Scene> GetScenes()
        {
            logger.LogTrace("Get scenes repository was called.");
            return _scenes.Values;
        }
    }
}
