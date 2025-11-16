using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvoStory.Database.Data;       
using EvoStory.Database.Models;     
using Microsoft.EntityFrameworkCore;
using EvoStory.Database.Exceptions;


namespace EvoStory.Database.Repository
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ChoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Choice> CreateChoice(Choice choice, Guid sceneId)
        {
            var scene = await _context.Scenes.Include(s => s.Choices).FirstOrDefaultAsync(s => s.Id == sceneId);
            if (scene == null)
            {
                throw new RepositoryException("Scene not found"); 
            }

            scene.Choices.Add(choice);
            await _context.SaveChangesAsync();
            return choice;
        }

        public async Task<Choice> GetChoice(Guid choiceId)
        {
            return await _context.Choises.FindAsync(choiceId);
        }

        public async Task<IEnumerable<Choice>> GetChoices()
        {
            return await _context.Choises.ToListAsync();
        }

        public async Task<Choice> DeleteChoice(Guid choiceId)
        {
            var choice = await _context.Choises.FindAsync(choiceId);
            if (choice != null)
            {
                _context.Choises.Remove(choice);
                await _context.SaveChangesAsync();
            }
            return choice;
        }
    }
}
