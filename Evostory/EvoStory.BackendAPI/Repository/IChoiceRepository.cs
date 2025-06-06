﻿using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Repository
{
    public interface IChoiceRepository
    {
        Choice? CreateChoice(Choice choice);
        Choice? GetChoice(Guid choiceId);
        IEnumerable<Choice> GetChoices();
        void DeleteChoice(Guid choiceId);
    }
}
