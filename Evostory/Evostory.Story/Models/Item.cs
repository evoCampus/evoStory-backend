using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvoStory.Database.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; 
        public string? Description { get; set; } 
        public bool IsStackable { get; set; } = true; 
        public Guid StoryId { get; set; }

        [ForeignKey("StoryId")]
        public Story Story { get; set; }
    }
}