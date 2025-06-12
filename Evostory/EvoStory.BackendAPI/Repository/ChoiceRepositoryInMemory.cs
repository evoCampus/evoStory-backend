using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;
using EvoStory.BackendAPI.Exceptions;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory(ILogger<ChoiceRepositoryInMemory> logger, IDatabase dbContext) : IChoiceRepository
    {
        public Choice CreateChoice(Choice choice, Guid sceneId)
        {
            logger.LogTrace("Create choice repository was called.");
            var story = dbContext.Stories.Values.FirstOrDefault(s => s.Scenes.Any(sc => sc.Id == sceneId));
            if (story is null)
            {
                logger.LogWarning($"Scene with Id: {sceneId} was not found when trying to create choice with Id: {choice.Id}.");
                throw new RepositoryException($"No scene with ID {sceneId} found.");
            }
            var scenes = story.Scenes.ToList();
            var scene = scenes.FirstOrDefault(s => s.Id == sceneId);
            if (scene is null)
            {
                logger.LogWarning($"Scene with Id: {sceneId} was not found in story with Id: {story.Id}.");
                throw new RepositoryException($"No scene with ID {sceneId} found in story with ID {story.Id}.");
            }
            var choices = scene.Choices.ToList();
            choices.Add(choice);
            scene.Choices = choices;
            scenes = scenes.Select(s => s.Id == sceneId ? scene : s).ToList();
            story.Scenes = scenes;
            logger.LogInformation($"Choice with Id: {choice.Id} was created in scene with Id: {sceneId} in story with Id: {story.Id}.");
            return choice;
        }

        public Choice GetChoice(Guid choiceId)
        {
            logger.LogTrace("Get choice repository was called.");
            foreach (var story in dbContext.Stories.Values)
            {
                foreach (var scene in story.Scenes)
                {
                    var choice = scene.Choices.FirstOrDefault(c => c.Id == choiceId);
                    if (choice != null)
                    {
                        return choice;
                    }
                }
            }
            logger.LogWarning($"Choice with Id: {choiceId} was not found in any scene.");
            throw new RepositoryException($"No choice with ID {choiceId} found.");
        }

        public IEnumerable<Choice> GetChoices()
        {
            logger.LogTrace("Get choices repository was called.");
            return dbContext.Stories.Values.SelectMany(story => story.Scenes.SelectMany(scene => scene.Choices));
        }

        public Choice DeleteChoice(Guid choiceId)
        {
            logger.LogTrace("Delete choice repository was called.");
            foreach (var story in dbContext.Stories.Values)
            {
                var scene = story.Scenes.FirstOrDefault(s => s.Choices.Any(c => c.Id == choiceId));
                if (scene != null)
                {
                    var choice = scene.Choices.FirstOrDefault(c => c.Id == choiceId);
                    if (choice != null)
                    {
                        scene.Choices = scene.Choices.Where(c => c.Id != choiceId);
                        dbContext.Stories.TryGetValue(story.Id, out var actualStory);
                        actualStory.Scenes = actualStory.Scenes.Select(s => s.Id == scene.Id ? scene : s);
                        logger.LogInformation($"Choice with Id: {choiceId} was deleted from scene with Id: {scene.Id} in story with Id: {story.Id}.");
                        return choice;
                    }
                }
            }
            logger.LogWarning($"Choice with Id: {choiceId} was not found in any scene.");
            throw new RepositoryException($"No choice with ID {choiceId} found.");
        }
    }
}
