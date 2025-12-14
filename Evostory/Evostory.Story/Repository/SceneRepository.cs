using EvoStory.Database.Data;
using EvoStory.Database.Exceptions;
using EvoStory.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoStory.Database.Repository
{
    public class SceneRepository : ISceneRepository
    {
        private readonly ApplicationDbContext _context;

        public SceneRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Scene> CreateScene(Scene scene, Guid storyId)
        {

            scene.StoryId = storyId;
            var storyExists = await _context.Stories.AnyAsync(s => s.Id == storyId);
            if (!storyExists)
            {
                throw new RepositoryException("Story not found");
            }

            scene.StoryId = storyId;

            _context.Scenes.Add(scene);

            await _context.SaveChangesAsync();

            return scene;
        }

        public async Task<Scene> GetScene(Guid sceneId) => await _context.Scenes
                    .Include(s => s.Content)
                    .Include(s => s.Choices)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == sceneId);

        public async Task<IEnumerable<Scene>> GetScenes()
        {
            return await _context.Scenes
              .Include(s => s.Content)
              .Include(s => s.Choices)  
              .AsNoTracking()           
              .ToListAsync();
        }

        public async Task<Scene> DeleteScene(Guid sceneId)
        {
            var scene = await _context.Scenes.FindAsync(sceneId);
            if (scene != null)
            {
                _context.Scenes.Remove(scene);
                await _context.SaveChangesAsync();
            }
            return scene;
        }
    }
}
