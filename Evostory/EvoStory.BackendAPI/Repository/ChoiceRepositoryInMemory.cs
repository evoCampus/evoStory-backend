using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory : IChoiceRepository
    {
        private List<Choice> choices = new();
        public void CreateChoice(Choice choice)
        {
            choices.Add(choice);
        }

        public Choice GetChoice(Guid choiceId)
        {
            var result = choices.FirstOrDefault(choice => choice.Id == choiceId);
            return result;
        }

        public IEnumerable<Choice> GetChoices()
        {
            return choices;
        }

        public void DeleteChoice(Guid choiceId)
        {
            var result = choices.FirstOrDefault(choice => choice.Id == choiceId);
            if (result == null)
            {
                throw new Exception("Choice Not Found");
            }
            choices.Remove(result);
        }
    }
}
