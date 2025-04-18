using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IChoiceService
    {
        void CreateChoice(CreateChoiceDTO choice);
    }
}
