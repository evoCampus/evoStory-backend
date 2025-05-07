using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory : IChoiceRepository
    {
        private Dictionary<Guid, Choice> _choices = new();
        public Choice CreateChoice(Choice choice)
        {
            _choices.Add(choice.Id, choice);
            return choice;
        }

        public Choice? GetChoice(Guid choiceId)
        {
            var result = _choices.FirstOrDefault(choice => choice.Key == choiceId);
            return result.Value;
        }

        public IEnumerable<Choice> GetChoices()
        {
            return _choices.Values;
        }

        public void DeleteChoice(Guid choiceId)
        {
            var result = _choices.FirstOrDefault(choice => choice.Key == choiceId);
            _choices.Remove(result.Key);
        }
    }
}
