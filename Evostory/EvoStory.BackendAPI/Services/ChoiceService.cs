using EvoStory.Database.Models;
using EvoStory.BackendAPI.Controllers;
using EvoStory.BackendAPI.DTO;
using EvoStory.Database.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EvoStory.BackendAPI.Services
{
    public class ChoiceService(IChoiceRepository choiceRepository, IDTOConversionService dTOConversion, ILogger<ChoiceService> logger, IInventoryService inventoryService) : IChoiceService
    {
        public async Task<ChoiceDTO> CreateChoice(CreateChoiceDTO choice)
        {
            logger.LogDebug("Create choice service was called.");
            var newChoice = new EvoStory.Database.Models.Choice 
            {
                Id = Guid.NewGuid(),
                NextSceneId = choice.NextSceneId,
                ChoiceText = choice.ChoiceText,
                RequiredItemId = choice.RequiredItemId
            };

            var createdChoice = await choiceRepository.CreateChoice(newChoice, choice.SceneId);

            logger.LogInformation($"Choice was created successfully with Id: {newChoice.Id}");
            return dTOConversion.ConvertChoiceToChoiceDTO(createdChoice);
        }

        public async Task<ChoiceDTO> GetChoice(Guid choiceId)
        {
            logger.LogDebug("Get choice service was called.");
            var result = await choiceRepository.GetChoice(choiceId);
            return dTOConversion.ConvertChoiceToChoiceDTO(result);
        }

        public async Task<IEnumerable<ChoiceDTO>> GetChoices()
        {
            logger.LogDebug("Get choices service was called.");
            var result = await choiceRepository.GetChoices();
            var choicesDTO = result.Select(choice => dTOConversion.ConvertChoiceToChoiceDTO(choice));
            return choicesDTO;
        }

        public async Task<ChoiceDTO> DeleteChoice(Guid choiceId)
        {
            logger.LogDebug("Delete choice service was called.");
            var result = await choiceRepository.DeleteChoice(choiceId);
            logger.LogInformation($"Choice with Id: {choiceId} was deleted.");
            return dTOConversion.ConvertChoiceToChoiceDTO(result);
        }
        public async Task<IEnumerable<ChoiceDTO>> GetAvailableChoicesForPlayer(Guid sceneId, Guid userId)
        {
            var allChoices = await choiceRepository.GetChoices();

            var playerInventory = await inventoryService.GetInventoryBySessionIdAsync(userId);

            var ownedItemIds = playerInventory.Select(i => i.ItemId).ToHashSet();

            var visibleChoices = new List<ChoiceDTO>();

            foreach (var choice in allChoices)
            {
                bool isVisible = false;

                if (choice.RequiredItemId == null)
                {
                    isVisible = true;
                }
                else
                {
                    if (ownedItemIds.Contains(choice.RequiredItemId.Value))
                    {
                        isVisible = true; 
                    }
                }

                if (isVisible)
                {
                    visibleChoices.Add(dTOConversion.ConvertChoiceToChoiceDTO(choice));
                }
            }

            return visibleChoices;
        }
    }
}
