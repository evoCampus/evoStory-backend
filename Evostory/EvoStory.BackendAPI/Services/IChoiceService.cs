using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IChoiceService
    {
        Task<ChoiceDTO> CreateChoice(CreateChoiceDTO choice);
        Task<ChoiceDTO>  GetChoice(Guid choiceId);
        Task<IEnumerable<ChoiceDTO>>  GetChoices();
        Task<ChoiceDTO> DeleteChoice(Guid choiceId);
        Task<IEnumerable<ChoiceDTO>> GetAvailableChoicesForPlayer(Guid sceneId, Guid userId);
        Task<Guid> SelectChoiceAsync(Guid userId, Guid choiceId);
    }
}
