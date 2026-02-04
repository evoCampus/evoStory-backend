using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EvoStory.BackendAPI.DTO
{
    public class AddToInventoryDTO
    {
        [Required]
        public Guid SessionId { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [Range(1, 5, ErrorMessage = "Quantity must be 1 or more.")]
        public int Quantity { get; set; } = 1;
    }
}