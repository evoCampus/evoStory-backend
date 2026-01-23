using EvoStory.Database.Models;
using System.Threading.Tasks;


namespace EvoStory.Database.Repository
{
    public interface ISceneRepository
    {
        public Task<Scene> CreateScene(Scene scene, Guid storyId);
        public Task<Scene> GetScene(Guid sceneId);
        public Task<IEnumerable<Scene>> GetScenes();
        public Task<Scene> DeleteScene(Guid sceneId);
    }
}
