

namespace Evostory.Story.Model
{
    class Story
    {
        public int StoryId { get; set; }
        public string Title { get; set; }
        public List<Scene> Scenes { get; set; }
        public int StartingSceneId { get; set; }

    }
}
