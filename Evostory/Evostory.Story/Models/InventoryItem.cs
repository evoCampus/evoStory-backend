using System.ComponentModel.DataAnnotations.Schema;

namespace EvoStory.Database.Models
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public int Quantity { get; set; } = 1;
    }
}