using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class ChoiceService(IChoiceRepository choiceRepository) : IChoiceService
    {
        public ChoiceDTO? CreateChoice(CreateChoiceDTO choice)
        {
            var newChoice = new Choice
            {
                Id = Guid.NewGuid(),
                NextSceneId = choice.NextSceneId,
                ChoiceText = choice.ChoiceText
            };

            choiceRepository.CreateChoice(newChoice);

            return new ChoiceDTO
            {
                Id = newChoice.Id,
                NextSceneId = newChoice.NextSceneId,
                ChoiceText = newChoice.ChoiceText
            };
        }

        public ChoiceDTO? GetChoice(Guid choiceId)
        {
            var result = choiceRepository.GetChoice(choiceId);
            if (result is null)
            {
                return null;
            }

            var choiceDTO = new ChoiceDTO
            {
                Id = result.Id,
                NextSceneId = result.NextSceneId,
                ChoiceText = result.ChoiceText
            };

            return choiceDTO;
        }

        public IEnumerable<ChoiceDTO> GetChoices()
        {
            var result = choiceRepository.GetChoices();
            var choicesDTO = result.Select(choice => new ChoiceDTO
            {
                Id = choice.Id,
                NextSceneId = choice.NextSceneId,
                ChoiceText = choice.ChoiceText
            });

            return choicesDTO;
        }

        public void DeleteChoice(Guid choiceId)
        {
            choiceRepository.DeleteChoice(choiceId);
        }
    }
}
