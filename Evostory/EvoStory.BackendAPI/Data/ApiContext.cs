using Microsoft.EntityFrameworkCore;
using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Story> Story { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
    }
}
