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

        public int Quantity { get; set; } = 1;
    }
}