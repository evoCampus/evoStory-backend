using Evostory.Story.Models;
using EvoStory.BackendAPI.Controllers;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class ChoiceService(IChoiceRepository choiceRepository, IDTOConversionService dTOConversion, ILogger<ChoiceController> logger) : IChoiceService
    {
        public ChoiceDTO CreateChoice(CreateChoiceDTO choice)
        {
            logger.LogDebug("Create choice service was called.");
            var newChoice = new Choice
            {
                Id = Guid.NewGuid(),
                NextSceneId = choice.NextSceneId,
                ChoiceText = choice.ChoiceText
            };

            choiceRepository.CreateChoice(newChoice);
            logger.LogInformation($"Choice was created successfully with Id: {newChoice.Id}");
            return dTOConversion.ConvertChoiceToChoiceDTO(newChoice);
        }

        public ChoiceDTO GetChoice(Guid choiceId)
        {
            logger.LogDebug("Get choice service was called.");
            var result = choiceRepository.GetChoice(choiceId);
            return dTOConversion.ConvertChoiceToChoiceDTO(result);
        }

        public IEnumerable<ChoiceDTO> GetChoices()
        {
            logger.LogDebug("Get choices service was called.");
            var result = choiceRepository.GetChoices();
            var choicesDTO = result.Select(choice => dTOConversion.ConvertChoiceToChoiceDTO(choice));
            return choicesDTO;
        }

        public ChoiceDTO DeleteChoice(Guid choiceId)
        {
            logger.LogDebug("Delete choice service was called.");
            var result = choiceRepository.DeleteChoice(choiceId);
            logger.LogInformation($"Choice with Id: {choiceId} was deleted.");
            return dTOConversion.ConvertChoiceToChoiceDTO(result);
        }
    }
}
