using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IInventoryService
    {
        Task<ItemDTO> CreateItemAsync(CreateItemDTO createItemDto);
        Task<IEnumerable<ItemDTO>> GetItemsByStoryIdAsync(Guid storyId);
        Task AddItemToInventoryAsync(AddToInventoryDTO dto, Guid userId);
        Task<List<InventoryItemDTO>> GetInventoryBySessionIdAsync(Guid sessionId);
        Task ClearInventoryAsync(Guid sessionId);
    }
}