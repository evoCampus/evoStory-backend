using System.ComponentModel.DataAnnotations;

namespace EvoStory.BackendAPI.DTO
{
    public class CreateItemDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsStackable { get; set; } = true;

        [Required]
        public Guid StoryId { get; set; }
    }
}