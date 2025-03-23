

namespace Evostory.Story.Model
{
    class Scene
    {
        public int SceneId { get; set; }
        public Content Content { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
