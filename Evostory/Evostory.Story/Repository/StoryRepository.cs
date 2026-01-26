using EvoStory.Database.Data;
using EvoStory.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoStory.Database.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private readonly ApplicationDbContext _context;

        public StoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Story> CreateStory(Story story)
        {
            await _context.Stories.AddAsync(story);
            await _context.SaveChangesAsync();
            return story;
        }

        public async Task<Story> GetStory(Guid storyId)
        {
            return await _context.Stories
                                 .Include(s => s.Scenes)
                                 .FirstOrDefaultAsync(s => s.Id == storyId);
        }

        public async Task<IEnumerable<Story>> GetStories()
        {
            return await _context.Stories.ToListAsync();
        }

        public async Task<Story> EditStory(Story story)
        {
            _context.Stories.Update(story);
            await _context.SaveChangesAsync();
            return story;
        }

        public async Task<Story?> DeleteStory(Guid storyId)
        {
            var story = await _context.Stories.FindAsync(storyId);
            if (story != null)
            {
                _context.Stories.Remove(story);
                await _context.SaveChangesAsync();
            }
            return story;
        }
    }
}
