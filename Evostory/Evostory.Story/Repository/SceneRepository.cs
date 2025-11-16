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
            var story = await _context.Stories.Include(s => s.Scenes).FirstOrDefaultAsync(s => s.Id == storyId);
            if (story == null)
            {
                throw new RepositoryException("Story not found");
            }

            story.Scenes.Add(scene);
            await _context.SaveChangesAsync();
            return scene;
        }

        public async Task<Scene> GetScene(Guid sceneId)
        {
            return await _context.Scenes
                                 .Include(s => s.Choices)
                                 .FirstOrDefaultAsync(s => s.Id == sceneId);
        }

        public async Task<IEnumerable<Scene>> GetScenes()
        {
            return await _context.Scenes.ToListAsync();
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
