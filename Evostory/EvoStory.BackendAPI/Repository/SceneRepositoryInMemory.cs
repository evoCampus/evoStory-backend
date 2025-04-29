using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory : ISceneRepository
    {
        private List<Scene> scenes = new();
        public void CreateScene(Scene scene)
        {
            scenes.Add(scene);
        }

        public void DeleteScene(Guid sceneId)
        {
            var result = scenes.FirstOrDefault(scene => scene.Id == sceneId);
            if (result == null)
            {
                throw new KeyNotFoundException($"No scene with {sceneId} found.");
            }
            scenes.Remove(result);
        }

        public Scene? GetScene(Guid sceneId)
        {
            var result = scenes.FirstOrDefault(scene => scene.Id == sceneId);
            return result;
        }

        public IEnumerable<Scene>? GetScenes()
        {
            return scenes;
        }
    }
}
