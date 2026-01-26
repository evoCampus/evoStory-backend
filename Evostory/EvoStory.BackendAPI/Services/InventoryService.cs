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

        public async Task<List<ItemDTO>> GetItemsByStoryIdAsync(Guid storyId)
        {
            var items = await _repository.GetItemsByStoryIdAsync(storyId);

            return items.Select(i => new ItemDTO
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                IsStackable = i.IsStackable
            }).ToList();
        }
        public async Task AddItemToInventoryAsync(AddToInventoryDTO dto)
        {
            var itemDefinition = await _repository.GetItemAsync(dto.ItemId);

            if (itemDefinition == null)
            {
                throw new KeyNotFoundException("Ilyen tárgy nem létezik az adatbázisban!");
            }

            var existingEntry = await _repository.GetInventoryItemAsync(dto.SessionId, dto.ItemId);

            if (!itemDefinition.IsStackable && (existingEntry != null || dto.Quantity > 1))
            {
                throw new InvalidOperationException("Ebbõl a tárgyból csak egy lehet nálad!");
            }

            if (existingEntry != null)
            {

                if ((long)existingEntry.Quantity + dto.Quantity > int.MaxValue)
                {
                    existingEntry.Quantity = int.MaxValue;
                }
                else
                {
                    existingEntry.Quantity += dto.Quantity;
                }

                await _repository.UpdateInventoryItemAsync(existingEntry);
            }
            else
            {
                var newEntry = new InventoryItem
                {
                    Id = Guid.NewGuid(),
                    SessionId = dto.SessionId,
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
        public async Task RemoveItemFromInventoryAsync(RemoveFromInventoryDTO dto, Guid userId)
        {
            var existingEntry = await _repository.GetInventoryItemAsync(userId, dto.ItemId);

            if (existingEntry == null)
            {
                throw new InvalidOperationException("Nincs ilyen tárgy a hátizsákodban!");
            }

            if (dto.RemoveAll)
            {
                await _repository.DeleteInventoryItemAsync(existingEntry);
                return;
            }

            if (existingEntry.Quantity < dto.Quantity)
            {
                throw new InvalidOperationException($"Nincs ennyi nálad! (Jelenleg: {existingEntry.Quantity} db)");
            }

            int newQuantity = existingEntry.Quantity - dto.Quantity;

            if (newQuantity > 0)
            {
                existingEntry.Quantity = newQuantity;
                await _repository.UpdateInventoryItemAsync(existingEntry);
            }
            else
            {
                await _repository.DeleteInventoryItemAsync(existingEntry);
            }
        }
    }
}