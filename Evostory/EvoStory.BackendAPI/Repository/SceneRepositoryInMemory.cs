using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;

namespace EvoStory.BackendAPI.Repository
{
    public class SceneRepositoryInMemory(ILogger<SceneRepositoryInMemory> logger) : ISceneRepository
    {
        private readonly DatabaseInMemory dbContext = new DatabaseInMemory();
        public Scene CreateScene(Scene scene, Guid storyId)
        {
            logger.LogTrace("Create scene repository was called.");
            if (dbContext.Stories.TryGetValue(storyId, out var story))
            {
                story.Scenes.Append(scene);
                logger.LogInformation($"Scene with Id: {scene.Id} was created in story with Id: {storyId}.");
                return scene;
            }
            else
            {
                logger.LogWarning($"Story with Id: {storyId} was not found when trying to create scene with Id: {scene.Id}.");
                throw new KeyNotFoundException($"No story with ID {storyId} found.");
            }
        }

        public Scene DeleteScene(Guid sceneId)
        {
            logger.LogTrace("Delete scene repository was called.");
            var story = dbContext.Stories.Values.FirstOrDefault(s => s.Scenes.Any(sc => sc.Id == sceneId));
            if (story != null)
            {
                dbContext.Stories.TryGetValue(story.Id, out var actualStory);
                var scene = story.Scenes.FirstOrDefault(s => s.Id == sceneId);
                actualStory.Scenes = story.Scenes.Where(s => s.Id != sceneId);
                logger.LogInformation($"Scene with Id: {sceneId} was deleted from story with Id: {story.Id}.");
                return scene;
            }
            else
            {
                logger.LogWarning($"Scene with Id: {sceneId} was not found in any story.");
                throw new KeyNotFoundException($"No scene with ID {sceneId} found.");
            }
        }

        public Scene? GetScene(Guid sceneId)
        {
            logger.LogTrace("Get scene repository was called.");
            foreach (var story in dbContext.Stories.Values)
            {
                var scene = story.Scenes.FirstOrDefault(s => s.Id == sceneId);
                if (scene != null)
                {
                    return scene;
                }
            }
            throw new KeyNotFoundException($"No scene with ID {sceneId} found.");
        }

        public IEnumerable<Scene> GetScenes()
        {
            logger.LogTrace("Get scenes repository was called.");
            return dbContext.Stories.Values.SelectMany(story => story.Scenes);
        }
    }
}
