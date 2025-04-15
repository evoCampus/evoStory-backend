using EvoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        public static List<Story> stories = new();

        [HttpPut]
        public ActionResult CreateStory(CreateStoryDTO story)
        {
            var newStory = new Story
            {
                Id = Guid.NewGuid(),
                Scenes = story.Scenes.Select(sceneDTO => new Scene()
                {
                    Choices = sceneDTO.Choices.Select(choiceDTO => new Choice()
                    {
                        ChoiceText = choiceDTO.ChoiceText,
                        Id = Guid.NewGuid(),
                        NextSceneId = choiceDTO.NextSceneId
                    }).ToList(),
                    Content = sceneDTO.Content,
                    Id = Guid.NewGuid()
                }),
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
            };
            stories.Add(newStory);
            return Created();
        }
        [HttpGet("{storyId}")]
        public ActionResult GetStory(Guid storyId)
        {
            var result = stories.FirstOrDefault(story => story.Id == storyId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public ActionResult GetStories()
        {
            return Ok(stories);
        }

        [HttpDelete("{storyId}")]
        public ActionResult DeleteStory(Guid storyId)
        {
            var result = stories.FirstOrDefault(story => story.Id == storyId);
            if (result == null)
            {
                return NotFound();
            }
            stories.Remove(result);
            return NoContent();
        }

        //Needs doing 
        [HttpPut("{storyId}")]
        public ActionResult EditStory(Guid storyId, EditStoryDTO story)
        {
            var existingStory = stories.FirstOrDefault(story => story.Id == storyId);
            var editedStory = new Story
            {
                Scenes = existingStory.Scenes,
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
            };
            existingStory.Title = editedStory.Title;
            existingStory.Scenes = editedStory.Scenes.ToList();
            existingStory.StartingSceneId = (Guid)editedStory.StartingSceneId;
            return Ok(existingStory);
        }
    }
}
