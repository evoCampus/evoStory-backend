using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IChoiceRepository
    {
        public Choice? CreateChoice(Choice choice);
        public Choice? GetChoice(Guid choiceId);
        public IEnumerable<Choice> GetChoices();
        public Choice DeleteChoice(Guid choiceId);
    }
}
