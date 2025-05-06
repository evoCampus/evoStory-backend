using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory : IChoiceRepository
    {
        private List<Choice> _choices = new();
        public void CreateChoice(Choice choice)
        {
            _choices.Add(choice);
        }

        public Choice? GetChoice(Guid choiceId)
        {
            var result = _choices.FirstOrDefault(choice => choice.Id == choiceId);
            return result;
        }

        public IEnumerable<Choice> GetChoices()
        {
            return _choices;
        }

        public void DeleteChoice(Guid choiceId)
        {
            var result = _choices.FirstOrDefault(choice => choice.Id == choiceId);
            if (result is null)
            {
                throw new KeyNotFoundException($"No choice with {choiceId} found.");
            }

            _choices.Remove(result);
        }
    }
}
