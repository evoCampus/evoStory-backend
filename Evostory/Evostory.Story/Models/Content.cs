namespace Evostory.Story.Models
{
    public class Content
    {
        public required Guid Id { get; set; }
        public string Text { get; set; }
        public Guid ImageId { get; set; }
        public Guid SoundId { get; set; }
    }
}
