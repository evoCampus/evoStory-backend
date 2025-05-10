using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class ChoiceService(IChoiceRepository choiceRepository, IDTOConversionService dTOConversion) : IChoiceService
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

            return dTOConversion.ConvertChoiceToChoiceDTO(newChoice);
        }

        public ChoiceDTO? GetChoice(Guid choiceId)
        {
            var result = choiceRepository.GetChoice(choiceId);
            if (result is null)
            {
                return null;
            }

            var choiceDTO = dTOConversion.ConvertChoiceToChoiceDTO(result);

            return choiceDTO;
        }

        public IEnumerable<ChoiceDTO> GetChoices()
        {
            var result = choiceRepository.GetChoices();
            var choicesDTO = result.Select(choice => dTOConversion.ConvertChoiceToChoiceDTO(choice));

            return choicesDTO;
        }

        public void DeleteChoice(Guid choiceId)
        {
            choiceRepository.DeleteChoice(choiceId);
        }
    }
}
