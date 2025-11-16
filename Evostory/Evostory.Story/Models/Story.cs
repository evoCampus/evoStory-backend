namespace EvoStory.Database.Models
{
    public class Story
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public virtual ICollection<Scene> Scenes { get; set; }
        public Guid StartingSceneId { get; set; }
        public Story()
        {
            Scenes = new List<Scene>();
        }
    }

}
