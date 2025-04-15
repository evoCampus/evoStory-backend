namespace EvoStory.BackendAPI.DTO
{
    public class ContentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid ImageId { get; set; }
        public Guid SoundId { get; set; }
    }
}
