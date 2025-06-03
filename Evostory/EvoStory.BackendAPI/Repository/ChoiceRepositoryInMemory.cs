using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;
using System.Runtime.CompilerServices;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory(ILogger<ChoiceRepositoryInMemory> logger) : IChoiceRepository
    {
        private readonly DatabaseInMemory dbContext = new DatabaseInMemory();
        public Choice CreateChoice(Choice choice)
        {
            logger.LogTrace("Create choice repository was called.");
            var story = dbContext.Stories.Values.FirstOrDefault(s => s.Scenes.Any(sc => sc.Id == choice.SceneId));
            if (story != null)
            {
                story.Scenes.FirstOrDefault(s => s.Id == choice.SceneId).Choices.Append(choice);
                dbContext.Stories.TryGetValue(story.Id, out var actualStory);
                actualStory = story;
                logger.LogInformation($"Choice with Id: {choice.Id} was created in scene with Id: {choice.SceneId} in story with Id: {story.Id}.");
                return choice;
            }
            else
            {
                logger.LogWarning($"Scene with Id: {choice.SceneId} was not found when trying to create choice with Id: {choice.Id}.");
                throw new KeyNotFoundException($"No scene with ID {choice.SceneId} found.");
            }
        }

        public Choice? GetChoice(Guid choiceId)
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
            throw new KeyNotFoundException($"No choice with ID {choiceId} found.");
        }

        public IEnumerable<Choice> GetChoices()
        {
            logger.LogTrace("Get choices repository was called.");
            return dbContext.Stories.Values.SelectMany(story=>story.Scenes.SelectMany(scene=>scene.Choices));
        }

        public Choice DeleteChoice(Guid choiceId)
        {
            logger.LogTrace("Delete choice repository was called.");
            foreach(var story in dbContext.Stories.Values)
            {
                var scene = story.Scenes.FirstOrDefault(s => s.Choices.Any(c => c.Id == choiceId));
                if (scene != null)
                {
                    var choice = scene.Choices.FirstOrDefault(c => c.Id == choiceId);
                    if (choice != null)
                    {
                        scene.Choices = scene.Choices.Where(c => c.Id != choiceId);
                        dbContext.Stories.TryGetValue(story.Id,out var actualStory);
                        actualStory.Scenes = actualStory.Scenes.Select(s => s.Id == scene.Id ? scene : s);
                        logger.LogInformation($"Choice with Id: {choiceId} was deleted from scene with Id: {scene.Id} in story with Id: {story.Id}.");
                        return choice;
                    }
                }
            }
            logger.LogWarning($"Choice with Id: {choiceId} was not found in any scene.");
            throw new KeyNotFoundException($"No choice with ID {choiceId} found.");
        }
    }
}
