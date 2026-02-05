namespace EvoStory.BackendAPI.DTO
{
    public class CreateSceneDTO
    {
        public CreateContentDTO? Content { get; set; }
        public int StoryId { get; set; }
        public IEnumerable<CreateChoiceDTO>? Choices { get; set; }
        public int SceneId { get; set; }

    }
}
