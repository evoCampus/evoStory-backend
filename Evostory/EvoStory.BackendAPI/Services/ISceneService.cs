using EvoStory.BackendAPI.DTO;
using System.Threading.Tasks;

namespace EvoStory.BackendAPI.Services
{
    public interface ISceneService
    {
        Task<SceneDTO> CreateScene(CreateSceneDTO scene);
        Task<SceneDTO> GetScene(Guid sceneId);
        Task<IEnumerable<SceneDTO>> GetScenes();
        Task<SceneDTO> DeleteScene(Guid sceneId);
    }
}
