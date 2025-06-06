﻿using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public class ChoiceRepositoryInMemory(ILogger<ChoiceRepositoryInMemory> logger) : IChoiceRepository
    {
        private Dictionary<Guid, Choice> _choices = new();
        public Choice CreateChoice(Choice choice)
        {
            logger.LogTrace("Create choice repository was called.");
            _choices.Add(choice.Id, choice);
            return choice;
        }

        public Choice? GetChoice(Guid choiceId)
        {
            logger.LogTrace("Get choice repository was called.");
            var result = _choices.FirstOrDefault(choice => choice.Key == choiceId);
            return result.Value;
        }

        public IEnumerable<Choice> GetChoices()
        {
            logger.LogTrace("Get choices repository was called.");
            return _choices.Values;
        }

        public void DeleteChoice(Guid choiceId)
        {
            logger.LogTrace("Delete choice repository was called.");
            var result = _choices.FirstOrDefault(choice => choice.Key == choiceId);
            _choices.Remove(result.Key);
        }
    }
}
