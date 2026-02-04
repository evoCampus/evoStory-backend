using EvoStory.BackendAPI.DTO;
using EvoStory.Database.Models;
using EvoStory.Database.Repository;

namespace EvoStory.BackendAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ItemDTO> CreateItemAsync(CreateItemDTO dto)
        {
            var newItem = new Item
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                IsStackable = dto.IsStackable,
                StoryId = dto.StoryId
            };

            var createdItem = await _repository.CreateItemAsync(newItem);

            return new ItemDTO
            {
                Id = createdItem.Id,
                Name = createdItem.Name,
                Description = createdItem.Description,
                IsStackable = createdItem.IsStackable
            };
        }

        public async Task<IEnumerable<ItemDTO>> GetItemsByStoryIdAsync(Guid storyId)
        {
            var items = await _repository.GetItemsByStoryIdAsync(storyId);

            return items.Select(i => new ItemDTO
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                IsStackable = i.IsStackable
            });
        }
        public async Task AddItemToInventoryAsync(AddToInventoryDTO dto, Guid userId)
        {
            var itemDefinition = await _repository.GetItemAsync(dto.ItemId);

            if (itemDefinition == null)
            {
                throw new KeyNotFoundException("Ilyen tárgy nem létezik az adatbázisban!");
            }

            var existingEntry = await _repository.GetInventoryItemAsync(userId, dto.ItemId);

            if (!itemDefinition.IsStackable && existingEntry != null)
            {
                return;
            }

            if (existingEntry != null)
            {
                if ((long)existingEntry.Quantity + dto.Quantity > int.MaxValue)
                    existingEntry.Quantity = int.MaxValue;
                else
                    existingEntry.Quantity += dto.Quantity;

                await _repository.UpdateInventoryItemAsync(existingEntry);
            }
            else
            {
                var newEntry = new InventoryItem
                {
                    Id = Guid.NewGuid(),
                    SessionId = userId,
                    ItemId = dto.ItemId,
                    Quantity = dto.Quantity
                };
                await _repository.AddInventoryItemAsync(newEntry);
            }
        }

        public async Task<List<InventoryItemDTO>> GetInventoryBySessionIdAsync(Guid sessionId)
        {
            var inventory = await _repository.GetInventoryBySessionIdAsync(sessionId);

            return inventory.Select(i => new InventoryItemDTO
            {
                ItemId = i.ItemId,
                ItemName = i.Item.Name,
                ItemDescription = i.Item.Description,
                Quantity = i.Quantity
            }).ToList();
        }
        public async Task ClearInventoryAsync(Guid sessionId)
        {
         
            await _repository.ClearInventoryAsync(sessionId);
        }
    }
}