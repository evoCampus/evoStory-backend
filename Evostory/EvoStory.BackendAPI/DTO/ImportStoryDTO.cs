namespace EvoStory.BackendAPI.DTO
{
    public class ImportStoryDTO
    {
        public string Title { get; set; }
        public IEnumerable<ImportSceneDTO> Scenes { get; set; }
        public int StartingSceneId { get; set; }
    }
}
