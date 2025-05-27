using Evostory.Story.Models;
using EvoStory.BackendAPI.Exceptions;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory : IChoiceRepository
    {
        private Dictionary<Guid, Choice> _choices = new();
        public Choice CreateChoice(Choice choice)
        {
            if (_choices.ContainsKey(choice.Id))
            {
                throw new RepositoryException($"Existing choice with {choice.Id} found.");
            }
            _choices.Add(choice.Id, choice);
            return choice;
        }

        public Choice GetChoice(Guid choiceId)
        {
            var result = _choices.FirstOrDefault(choice => choice.Key == choiceId);
            if (result.Value is null)
            {
                throw new RepositoryException($"No choice with {choiceId} found.");
            }

            return result.Value;
        }

        public IEnumerable<Choice> GetChoices()
        {
            return _choices.Values;
        }

        public Choice DeleteChoice(Guid choiceId)
        {
            var result = _choices.FirstOrDefault(choice => choice.Key == choiceId);
            if (result.Value is null)
            {
                throw new RepositoryException($"No choice with {choiceId} found.");
            }

            _choices.Remove(result.Key);
            return result.Value;
        }
    }
}
