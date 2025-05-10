using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IChoiceService
    {
        ChoiceDTO? CreateChoice(CreateChoiceDTO choice);
        ChoiceDTO? GetChoice(Guid choiceId);
        IEnumerable<ChoiceDTO> GetChoices();
        void DeleteChoice(Guid choiceId);
    }
}
