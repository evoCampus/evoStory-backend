using Evostory.Story.Models;
using Microsoft.EntityFrameworkCore;

// Gyõzõdj meg róla, hogy a namespace a te projektednek megfelelõ!
namespace EvoStory.BackendAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Ez a konstruktor kell ahhoz, hogy a Program.cs-ben megadott
        // beállításokat (pl. a kapcsolati stringet) megkapja.
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