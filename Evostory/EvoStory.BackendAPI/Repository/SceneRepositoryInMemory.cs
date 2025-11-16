using EvoStory.Database.Models;
using EvoStory.BackendAPI.Database;
using EvoStory.Database.Exceptions;
using EvoStory.Database.Repository;
using System.Threading.Tasks;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory(ILogger<SceneRepositoryInMemory> logger, IDatabase dbContext) : ISceneRepository
    {
        public async Task<Scene> CreateScene(Scene scene, Guid storyId)
        {
            logger.LogTrace("Create scene repository was called.");
            if (dbContext.Stories.TryGetValue(storyId, out var story))
            {
                var scenes = story.Scenes.ToList();
                scenes.Add(scene);
                story.Scenes = scenes;
                logger.LogInformation($"Scene with Id: {scene.Id} was created in story with Id: {storyId}.");
                return scene;
            }
            logger.LogWarning($"Story with Id: {storyId} was not found when trying to create scene with Id: {scene.Id}.");
            throw new RepositoryException($"No story with ID {storyId} found.");
        }

        public Task<Scene> DeleteScene(Guid sceneId)
        {
            logger.LogTrace("Delete scene repository was called.");
            var story = dbContext.Stories.Values.FirstOrDefault(s => s.Scenes.Any(sc => sc.Id == sceneId));
            if (story is null)
            {
                logger.LogWarning($"Scene with Id: {sceneId} was not found in any story.");
                throw new RepositoryException($"No scene with ID {sceneId} found.");
            }
            var scene = story.Scenes.FirstOrDefault(s => s.Id == sceneId);
            story.Scenes = story.Scenes.Where(s => s.Id != sceneId).ToList();
            logger.LogInformation($"Scene with Id: {sceneId} was deleted from story with Id: {story.Id}.");
            return Task.FromResult(scene)!;
        }

        public Task<Scene> GetScene(Guid sceneId)
        {
            logger.LogTrace("Get scene repository was called.");
            foreach (var story in dbContext.Stories.Values)
            {
                var scene = story.Scenes.FirstOrDefault(s => s.Id == sceneId);
                if (scene != null)
                {
                    return Task.FromResult(scene);
                }
            }
            logger.LogWarning($"Scene with Id: {sceneId} was not found in any story.");
            throw new RepositoryException($"No scene with ID {sceneId} found.");
        }

        public Task<IEnumerable<Scene>> GetScenes()
        {
            logger.LogTrace("Get scenes repository was called.");
            return Task.FromResult(dbContext.Stories.Values.SelectMany(story => story.Scenes));
        }
    }
}
