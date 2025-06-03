using Evostory.Story.Models;
using EvoStory.BackendAPI.Database;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory(IDatabase dbContext) : IChoiceRepository
    {
        public Choice CreateChoice(Choice choice)
        {
            dbContext.AddChoice(choice);
            return choice;
        }

        public Choice? GetChoice(Guid choiceId)
        {
            var result = dbContext.GetChoice(choiceId);
            return result;
        }

        public IEnumerable<Choice> GetChoices()
        {
            return dbContext.GetAllChoices();
        }

        public Choice DeleteChoice(Guid choiceId)
        {
            var result = dbContext.RemoveChoice(choiceId);
            return result;
        }
    }
}
