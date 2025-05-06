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

        public ChoiceDTO? GetChoice(Guid choiceId)
        {
            var result = choiceRepository.GetChoice(choiceId);
            if (result is null)
            {
                return null;
            }

            var choice = new ChoiceDTO
            {
                Id = result.Id,
                NextSceneId = result.NextSceneId,
                ChoiceText = result.ChoiceText
            };

            return choice;
        }

        public IEnumerable<ChoiceDTO>? GetChoices()
        {
            var result = choiceRepository.GetChoices();
            if (result is null)
            {
                return null;
            }

            var choices = result.Select(choice => new ChoiceDTO
            {
                Id = choice.Id,
                NextSceneId = choice.NextSceneId,
                ChoiceText = choice.ChoiceText
            });

            return choices;
        }

        public void DeleteChoice(Guid choiceId)
        {
            choiceRepository.DeleteChoice(choiceId);
        }
    }
}
