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
                NextSceneId = null,
                ChoiceText = choice.ChoiceText,
                RequiredItemId = choice.RequiredItemId
            };

            var createdChoice = await choiceRepository.CreateChoice(newChoice, Guid.Empty);

            logger.LogInformation($"Choice was created successfully with Id: {newChoice.Id}");
            return dTOConversion.ConvertChoiceToChoiceDTO(createdChoice);
        }

        public async Task<ChoiceDTO> GetChoice(Guid choiceId)
        {
            Console.WriteLine($"--- Searching: {choiceId} ---");
            logger.LogDebug("Get choice service was called.");
            var result = await choiceRepository.GetChoice(choiceId);
            if (result == null)
            {
                Console.WriteLine("--- Did not find! (NULL) ---");
                return null;
            }
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
            var allChoices = await choiceRepository.GetChoicesBySceneId(sceneId);

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
        public async Task<Guid> SelectChoiceAsync(Guid userId, Guid choiceId)
        {
            var choice = await choiceRepository.GetChoice(choiceId);

            if (choice == null)
            {
                throw new Exception("A választott lehetõség nem található.");
            }

            if (choice.RewardItemId != null)
            {
                logger.LogInformation($"A játékos ({userId}) jutalmat kap: {choice.RewardItemId}");

                var playerInventory = await inventoryService.GetInventoryBySessionIdAsync(userId);
                var existingItem = playerInventory.FirstOrDefault(i => i.ItemId == choice.RewardItemId.Value);

                if (existingItem == null)
                {
                    var newItemDto = new AddToInventoryDTO
                    {
                        SessionId = userId,
                        ItemId = choice.RewardItemId.Value,
                        Quantity = 1
                    };

                    await inventoryService.AddItemToInventoryAsync(newItemDto, userId);
                }
            }

            return choice.NextSceneId ?? Guid.Empty;
        }
    }
}
