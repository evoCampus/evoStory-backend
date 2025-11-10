using Microsoft.EntityFrameworkCore;
using EvoStory.Database.Models;

namespace EvoStory.Database.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Story> Story { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
    }
}
