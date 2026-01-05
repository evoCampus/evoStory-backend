using System.ComponentModel.DataAnnotations.Schema;

namespace EvoStory.Database.Models
{
    public class Scene
    {
        public required Guid Id { get; set; }

        public Content Content { get; set; }
        public Guid StoryId { get; set; }

        [ForeignKey("StoryId")]
        public Story Story { get; set; }
        public ICollection<Choice> Choices { get; set; } = new List<Choice>();

    }
}
