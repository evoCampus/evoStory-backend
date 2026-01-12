using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EvoStory.BackendAPI.DTO
{
    public class AddToInventoryDTO
    {
        [Required]
        [JsonIgnore]
        public Guid SessionId { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [Range(1, 5, ErrorMessage = "A mennyiségnek legalább 1-nek kell lennie, de 5-nél több nem lehet!")]
        public int Quantity { get; set; } = 1;
    }
}