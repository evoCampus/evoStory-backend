using EvoStory.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoStory.Database.Data
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Scene>()
                .HasOne(s => s.Story)
                .WithMany(story => story.Scenes)
                .HasForeignKey(s => s.StoryId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Choice>()
                .HasOne(c => c.Scene)
                .WithMany(s => s.Choices)
                .HasForeignKey(c => c.SceneId)
                .OnDelete(DeleteBehavior.Cascade); 

          
            modelBuilder.Entity<Choice>()
                .HasOne(c => c.NextScene)
                .WithMany() 
                .HasForeignKey(c => c.NextSceneId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }

    }