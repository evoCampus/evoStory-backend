using Evostory.Story.Models;
using EvoStory.BackendAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        public static List<Story> stories = new();
        /// <summary>
        /// Creates a Story.
        /// </summary>
        /// <param name="story"></param>
        /// <response code="204">The Story was successfully created.</response>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(StoryDTO), StatusCodes.Status204NoContent)]
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
                    Content = new Content
                    {
                        Id = Guid.NewGuid(),
                        Text = sceneDTO.Content.Text,
                        ImageId = sceneDTO.Content.ImageId,
                        SoundId = sceneDTO.Content.SoundId
                    },
                    Id = Guid.NewGuid()
                }),
                StartingSceneId = story.StartingSceneId ?? Guid.NewGuid(),
                Title = story.Title
            };
            stories.Add(newStory);
            return Created();
        }

        /// <summary>
        /// Get Story by Id.
        /// </summary>
        /// <param name="storyId"></param>
        /// <response code="200">The Story was successfully retrieved.</response>
        /// <response code="404">Story not found.</response>
        [HttpGet("{storyId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(StoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetStory(Guid storyId)
        {
            var result = stories.FirstOrDefault(story => story.Id == storyId);
            if (result == null)
            {
                return NotFound();
            }

            var storyDTO = new StoryDTO
            {
                Id = result.Id,
                Scenes = result.Scenes.Select(scene => new SceneDTO()
                {
                    Choices = scene.Choices.Select(choice => new ChoiceDTO()
                    {
                        ChoiceText = choice.ChoiceText,
                        Id = choice.Id,
                        NextSceneId = choice.NextSceneId
                    }).ToList(),
                    Content = new ContentDTO
                    {
                        Id = scene.Content.Id,
                        Text = scene.Content.Text,
                        ImageId = scene.Content.ImageId,
                        SoundId = scene.Content.SoundId
                    },
                    Id = scene.Id
                }),
                StartingSceneId = result.StartingSceneId,
                Title = result.Title
            };
            return Ok(storyDTO);
        }

        /// <summary>
        /// Get all Stories.
        /// </summary>
        /// <response code="200">The Stories were successfully retrieved..</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<StoryDTO>), StatusCodes.Status200OK)]
        public ActionResult GetStories()
        {
            var storiesDTO = stories.Select(story => new StoryDTO
            {
                Id = story.Id,
                Scenes = story.Scenes.Select(scene => new SceneDTO()
                {
                    Choices = scene.Choices.Select(choice => new ChoiceDTO()
                    {
                        ChoiceText = choice.ChoiceText,
                        Id = choice.Id,
                        NextSceneId = choice.NextSceneId
                    }).ToList(),
                    Content = new ContentDTO
                    {
                        Id = scene.Content.Id,
                        Text = scene.Content.Text,
                        ImageId = scene.Content.ImageId,
                        SoundId = scene.Content.SoundId
                    },
                    Id = scene.Id
                }),
                StartingSceneId = story.StartingSceneId,
                Title = story.Title
            });
            return Ok(storiesDTO);
        }

        /// <summary>
        /// Deletes a Story by Id.
        /// </summary>
        /// <param name="storyId"></param>
        /// <response code="204">The Story was successfully deleted.</response>
        /// <response code="404">Story not found.</response>
        [HttpDelete("{storyId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Edits an existing story.
        /// </summary>
        /// <param name="storyId"></param>
        /// <param name="story"></param>
        /// <response code="200">The Story was successfully edited.</response>
        [HttpPut("{storyId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(StoryDTO), StatusCodes.Status200OK)]
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
            existingStory.StartingSceneId = editedStory.StartingSceneId;
            return Ok(existingStory);
        }
    }
}
