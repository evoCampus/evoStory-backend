using System.ComponentModel.DataAnnotations;

namespace EvoStory.BackendAPI.DTO
{
    public class AddToInventoryDTO
    {
        [Required]
        public Guid SessionId { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}