using System.ComponentModel.DataAnnotations;

namespace EvoStory.BackendAPI.DTO
{
    public class RemoveFromInventoryDTO
    {
        [Required]
        public Guid ItemId { get; set; }

        [Required]
        [Range(1, 9999, ErrorMessage = "Legalább 1 darabot kell eldobnod.")]
        public int Quantity { get; set; }
        public bool RemoveAll { get; set; } = false;
    }
}