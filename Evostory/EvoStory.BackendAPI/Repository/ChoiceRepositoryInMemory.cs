using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory(IDatabase dbContext,ILogger<ChoiceRepositoryInMemory> logger) : IChoiceRepository
    {
        public Choice CreateChoice(Choice choice)
        {
            logger.LogTrace("Create choice repository was called.");
            dbContext.AddChoice(choice);
            return choice;
        }

        public Choice? GetChoice(Guid choiceId)
        {
            logger.LogTrace("Get choice repository was called.");
            var result = dbContext.GetChoice(choiceId);
            return result;
        }

        public IEnumerable<Choice> GetChoices()
        {
            logger.LogTrace("Get choices repository was called.");
            return dbContext.GetAllChoices();
        }

        public Choice DeleteChoice(Guid choiceId)
        {
            logger.LogTrace("Delete choice repository was called.");
            var result = dbContext.RemoveChoice(choiceId);
            return result;
        }
    }
}
