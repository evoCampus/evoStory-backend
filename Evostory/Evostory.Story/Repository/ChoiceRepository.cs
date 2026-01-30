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
            var sceneExists = await _context.Scenes.AnyAsync(s => s.Id == sceneId);
            if (!sceneExists)
            {
                throw new RepositoryException("Scene not found");
            }


            choice.SceneId = sceneId;

            await _context.Choises.AddAsync(choice);

            await _context.SaveChangesAsync();

            return choice;
        }

        public async Task<Choice> GetChoice(Guid choiceId)
        {
            Console.WriteLine($"[REPO] SQL Lekérdezés indítása ehhez az ID-hoz: {choiceId}");
            var choice = await _context.Choises
                   .AsNoTracking()
                   .Include(c => c.RewardItem)
                   .Include(c => c.RequiredItem)
                   .FirstOrDefaultAsync(c => c.Id == choiceId);

            if (choice == null)
            {
                Console.WriteLine("[REPO] EREDMÉNY: Az adatbázisban NEM található ilyen ID!");
            }
            else
            {
                Console.WriteLine($"[REPO] EREDMÉNY: Megtalálva! Jutalma: {(choice.RewardItem != null ? choice.RewardItem.Name : "Nincs")}");
            }

            return choice;
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
        public async Task<IEnumerable<Choice>> GetChoicesBySceneId(Guid sceneId)
        {
            return await _context.Choises
                                 .Where(c => c.SceneId == sceneId)
                                 .ToListAsync();
        }
    }
}
