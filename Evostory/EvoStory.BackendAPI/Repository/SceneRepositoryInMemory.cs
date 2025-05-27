using Evostory.Story.Models;
using EvoStory.BackendAPI.Exceptions;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory : ISceneRepository
    {
        private Dictionary<Guid, Scene> _scenes = new();
        public Scene CreateScene(Scene scene)
        {
            _scenes.Add(scene.Id, scene);
            return scene;
        }

        public Scene DeleteScene(Guid sceneId)
        {
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
            var result = _scenes.FirstOrDefault(scene => scene.Key == sceneId);
            if (result.Value is null)
            {
                throw new RepositoryException($"No scene with {sceneId} found.");
            }

            return result.Value;
        }

        public IEnumerable<Scene> GetScenes()
        {
            return _scenes.Values;
        }
    }
}
