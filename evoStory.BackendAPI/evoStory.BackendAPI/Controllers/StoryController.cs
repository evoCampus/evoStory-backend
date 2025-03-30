using evoStory.BackendAPI.DTO;
using evoStory.BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace evoStory.BackendAPI.Controllers
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
                Scenes = story.Scenes.ToList(),
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

        //new

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
        
        //Didnt manage to understand fully
        /*
        [HttpPatch("{storyId}")]
        public ActionResult EditStory(Guid storyId, EditStoryDTO story)
        {
            var existingStory = stories.FirstOrDefault(story => story.Id == storyId);

            var editedStory = new Story
            {
                Scenes = story.Scenes.ToList(),
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
                
            };

            existingStory.Title = story.Title;
            existingStory.Scenes = story.Scenes.ToList();
            existingStory.StartingSceneId = (Guid)story.StartingSceneId;
            
            return Ok(existingStory);
        }
        */
    }
}
