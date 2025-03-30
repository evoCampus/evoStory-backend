using Microsoft.EntityFrameworkCore;
using evoStory.BackendAPI.Models;

namespace evoStory.BackendAPI.Data
{
    public class ApiContext : DbContext
    {

        public DbSet<Story> Story { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        
    }
}
