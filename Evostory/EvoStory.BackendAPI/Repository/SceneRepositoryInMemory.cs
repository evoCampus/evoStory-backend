using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory : ISceneRepository
    {
        private Dictionary<Guid, Scene> scenes = new();
        public Scene CreateScene(Scene scene)
        {
            scenes.Add(scene.Id,scene);
            return scene;
        }

        public void DeleteScene(Guid sceneId)
        {
            var result = scenes.FirstOrDefault(scene => scene.Key == sceneId);
            scenes.Remove(result.Key);
        }

        public Scene? GetScene(Guid sceneId)
        {
            var result = scenes.FirstOrDefault(scene => scene.Key == sceneId);
            return result.Value;
        }

        public IEnumerable<Scene> GetScenes()
        {
            return scenes.Values;
        }
    }
}
