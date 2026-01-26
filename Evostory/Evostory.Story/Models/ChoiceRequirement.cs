using System.ComponentModel.DataAnnotations.Schema;

namespace EvoStory.Database.Models
{
    public class ChoiceRequirement
    {
        public Guid Id { get; set; }
        public Guid ChoiceId { get; set; }

        [ForeignKey("ChoiceId")]
        public Choice Choice { get; set; }
        public Guid RequiredItemId { get; set; }

        [ForeignKey("RequiredItemId")]
        public Item RequiredItem { get; set; }
        public int RequiredQuantity { get; set; } = 1;
        public bool ConsumeOnUse { get; set; } = false;
    }
}