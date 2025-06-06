using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IChoiceRepository
    {
        Choice? CreateChoice(Choice choice, Guid sceneId);
        Choice? GetChoice(Guid choiceId);
        IEnumerable<Choice> GetChoices();
        Choice DeleteChoice(Guid choiceId);
    }
}
