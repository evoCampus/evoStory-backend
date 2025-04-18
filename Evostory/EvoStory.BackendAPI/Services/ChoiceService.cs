using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class ChoiceService(IChoiceRepository choiceRepository) : IChoiceService
    {
        public void CreateChoice(CreateChoiceDTO choice)
        {
            var newChoice = new Choice
            {
                Id = Guid.NewGuid(),
                NextSceneId = choice.NextSceneId,
                ChoiceText = choice.ChoiceText
            };

            choiceRepository.CreateChoice(newChoice);
        }
    }
}
