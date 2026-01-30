using EvoStory.Database.Models;
using Microsoft.EntityFrameworkCore;
using EvoStory.Database.Data;
using System.Threading.Tasks;

namespace EvoStory.Database.Repository
{
    public interface IChoiceRepository
    {
        Task<Choice> CreateChoice(Choice choice, Guid sceneId);
        Task<Choice> GetChoice(Guid choiceId);
        Task<IEnumerable<Choice>> GetChoices();
        Task<Choice> DeleteChoice(Guid choiceId);
        Task<IEnumerable<Choice>> GetChoicesBySceneId(Guid sceneId);
    }
}
