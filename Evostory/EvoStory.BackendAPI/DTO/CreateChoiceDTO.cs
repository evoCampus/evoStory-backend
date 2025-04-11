using evoStory.BackendAPI.Models;

namespace evoStory.BackendAPI.DTO
{
    public class CreateChoiceDTO
    {
        public Guid NextSceneId { get; set; }
        public string? ChoiceText { get; set; }
    }
}
