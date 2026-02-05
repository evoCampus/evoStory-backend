using EvoStory.Database.Data;
using EvoStory.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoStory.Database.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<Item>> GetItemsByStoryIdAsync(Guid storyId)
        {
            return await _context.Items
                .Where(i => i.StoryId == storyId)
                .ToListAsync();
        }
        public async Task<InventoryItem?> GetInventoryItemAsync(Guid sessionId, Guid itemId)
        {
            return await _context.InventoryItems
                .FirstOrDefaultAsync(ii => ii.SessionId == sessionId && ii.ItemId == itemId);
        }

        public async Task AddInventoryItemAsync(InventoryItem inventoryItem)
        {
            _context.InventoryItems.Add(inventoryItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInventoryItemAsync(InventoryItem inventoryItem)
        {
            _context.InventoryItems.Update(inventoryItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<InventoryItem>> GetInventoryBySessionIdAsync(Guid sessionId)
        {
            return await _context.InventoryItems
                .Include(ii => ii.Item) 
                .Where(ii => ii.SessionId == sessionId)
                .ToListAsync();
        }
        public async Task<Item?> GetItemAsync(Guid id)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task ClearInventoryAsync(Guid userId)
        {
            var items = _context.InventoryItems.Where(i => i.SessionId == userId);
            _context.InventoryItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}