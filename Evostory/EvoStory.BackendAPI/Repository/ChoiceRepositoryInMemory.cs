using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory : IChoiceRepository
    {
        public void CreateChoice(Choice choice)
        {
            choices.Add(choice);
        }

        private List<Choice> choices = new();
    }
}
