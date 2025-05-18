using Evostory.Story.Models;
using EvoStory.BackendAPI.Controllers;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class ChoiceService(IChoiceRepository choiceRepository, IDTOConversionService dTOConversion, ILogger<ChoiceController> logger) : IChoiceService
    {
        public ChoiceDTO? CreateChoice(CreateChoiceDTO choice)
        {
            logger.LogInformation("Create choice service was called.");
            var newChoice = new Choice
            {
                Id = Guid.NewGuid(),
                NextSceneId = choice.NextSceneId,
                ChoiceText = choice.ChoiceText
            };

            choiceRepository.CreateChoice(newChoice);
            logger.LogDebug($"Choice was created successfully with Id: {newChoice.Id}");

            return dTOConversion.ConvertChoiceToChoiceDTO(newChoice);
        }

        public ChoiceDTO? GetChoice(Guid choiceId)
        {
            logger.LogInformation("Get choice service was called.");
            var result = choiceRepository.GetChoice(choiceId);
            if (result is null)
            {
                logger.LogWarning($"Choice with Id: {choiceId} was not found.");
                return null;
            }

            var choiceDTO = dTOConversion.ConvertChoiceToChoiceDTO(result);
            logger.LogDebug($"Choice with Id: {choiceId} was found.");

            return choiceDTO;
        }

        public IEnumerable<ChoiceDTO> GetChoices()
        {
            logger.LogInformation("Get choices service was called.");
            var result = choiceRepository.GetChoices();
            var choicesDTO = result.Select(choice => dTOConversion.ConvertChoiceToChoiceDTO(choice));
            logger.LogDebug("Choices were found.");

            return choicesDTO;
        }

        public void DeleteChoice(Guid choiceId)
        {
            logger.LogInformation("Delete choice service was called.");
            choiceRepository.DeleteChoice(choiceId);
            logger.LogDebug($"Choice with Id: {choiceId} was deleted.");
        }
    }
}
