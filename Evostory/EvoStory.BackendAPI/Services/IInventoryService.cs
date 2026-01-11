using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Services
{
    public interface IInventoryService
    {
        Task<ItemDTO> CreateItemAsync(CreateItemDTO createItemDto);
        Task<List<ItemDTO>> GetItemsByStoryIdAsync(Guid storyId);
        Task AddItemToInventoryAsync(AddToInventoryDTO dto);
        Task<List<InventoryItemDTO>> GetInventoryBySessionIdAsync(Guid sessionId);
    }
}