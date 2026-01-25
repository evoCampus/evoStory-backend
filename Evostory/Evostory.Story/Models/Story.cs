using System.ComponentModel.DataAnnotations;

namespace EvoStory.Database.Models
{
    public class Story
    {
        public required Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Title { get; set; } = string.Empty;
        public virtual ICollection<Scene> Scenes { get; set; } = new List<Scene>();
        public Guid StartingSceneId { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();

    }

}
