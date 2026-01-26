using EvoStory.Database.Models;

namespace EvoStory.Database.Repository
{
    public interface IInventoryRepository
    {
        Task<Item> CreateItemAsync(Item item);
        Task<List<Item>> GetItemsByStoryIdAsync(Guid storyId);
        Task<InventoryItem?> GetInventoryItemAsync(Guid sessionId, Guid itemId);
        Task AddInventoryItemAsync(InventoryItem inventoryItem);
        Task UpdateInventoryItemAsync(InventoryItem inventoryItem);
        Task<List<InventoryItem>> GetInventoryBySessionIdAsync(Guid sessionId);
        Task<Item?> GetItemAsync(Guid id);
        Task DeleteInventoryItemAsync(InventoryItem inventoryItem);
    }
}