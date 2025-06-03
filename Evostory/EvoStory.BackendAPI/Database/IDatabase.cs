using Evostory.Story.Models;
namespace EvoStory.BackendAPI.Database
{
    public interface IDatabase
    {
        public void AddStory(Story story);
        public Story RemoveStory(Guid storyId);
        public Story GetStory(Guid storyId);
        public IEnumerable<Story> GetAllStories();
        public Story UpdateStory(Story story);
        public void AddScene(Scene scene);
        public Scene RemoveScene(Guid sceneId);
        public Scene GetScene(Guid sceneId);
        public IEnumerable<Scene> GetAllScenes();
        public void AddChoice(Choice choice);
        public Choice RemoveChoice(Guid choiceId);
        public Choice GetChoice(Guid choiceId);
        public IEnumerable<Choice> GetAllChoices();
    }
}
