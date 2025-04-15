namespace EvoStory.BackendAPI.DTO
{
    public class CreateContentDTO
    {
        public string Text { get; set; }
        public Guid ImageId { get; set; }
        public Guid SoundId { get; set; }
    }
}
