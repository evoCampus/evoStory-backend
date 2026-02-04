using System.ComponentModel.DataAnnotations;

namespace EvoStory.BackendAPI.DTO
{
    public class RemoveFromInventoryDTO
    {
        [Required]
        public Guid ItemId { get; set; }

        [Required]
        [Range(1, 9999, ErrorMessage = "You must drop at least one.")]
        public int Quantity { get; set; }
        public bool RemoveAll { get; set; } = false;
    }
}