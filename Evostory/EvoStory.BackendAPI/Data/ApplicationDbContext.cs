using Evostory.Story.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoStory.BackendAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Choice> Choises { get; set; }
        public DbSet<Content> Contents{ get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Story> Stories { get; set; }
    }
}