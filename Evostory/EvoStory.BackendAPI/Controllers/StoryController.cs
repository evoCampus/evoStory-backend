using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowedOrigins")]
    public class StoryController(IStoryService storyService, ILogger<StoryController> logger) : ControllerBase
    {
        /// <summary>
        /// Creates a Story.
        /// </summary>
        /// <param name="story"></param>
        /// <response code="204">The Story was successfully created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPut(Name = nameof(CreateStory))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateStory(CreateStoryDTO story)
        {
            logger.LogInformation($"Creating story with Title: {story.Title};");
            try
            {
                storyService.CreateStory(story);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                logger.LogError(ex, "An error occurred when creating the story.");
                return BadRequest();
            }

            logger.LogDebug($"Story was created successfully with Title: {story.Title};");
            return Created();
        }

        /// <summary>
        /// Get Story by Id.
        /// </summary>
        /// <param name="storyId"></param>
        /// <response code="200">The Story was successfully retrieved.</response>
        /// <response code="404">Story not found.</response>
        [HttpGet("{storyId}", Name = nameof(GetStory))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(StoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetStory(Guid storyId)
        {
            logger.LogInformation($"Getting story with Id: {storyId}.");
            var result = storyService.GetStory(storyId);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get all Stories.
        /// </summary>
        /// <response code="200">The Stories were successfully retrieved.</response>        
        [HttpGet(Name = nameof(GetStories))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<StoryDTO>), StatusCodes.Status200OK)]
        public ActionResult GetStories()
        {
            logger.LogInformation("Getting all the stories.");
            var result = storyService.GetStories();
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Deletes a Story by Id.
        /// </summary>
        /// <param name="storyId"></param>
        /// <response code="204">The Story was successfully deleted.</response>
        /// <response code="404">Story not found.</response>
        [HttpDelete("{storyId}", Name = nameof(DeleteStory))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStory(Guid storyId)
        {
            logger.LogInformation($"Deleting story with Id: {storyId}.");
            try
            {
                storyService.DeleteStory(storyId);
            }
            catch (KeyNotFoundException)
            {
                logger.LogError($"Story with Id: {storyId} not found.");
                return NotFound();
            }

            logger.LogDebug($"Story with Id: {storyId} was deleted.");
            return NoContent();
        }

        /// <summary>
        /// Edits an existing story.
        /// </summary>
        /// <param name="storyId"></param>
        /// <param name="story"></param>
        /// <response code="200">The Story was successfully edited.</response>
        /// <response code="404">Story not found.</response>
        [HttpPut("{storyId}", Name = nameof(EditStory))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(StoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult EditStory(Guid storyId, EditStoryDTO story)
        {
            logger.LogInformation($"Editing story with Id: {storyId}.");
            try
            {
                storyService.EditStory(story);
            }
            catch (KeyNotFoundException)
            {
                logger.LogError($"Story with Id: {storyId} not found.");
                return NotFound();
            }

            logger.LogDebug($"Story with Id: {storyId} was edited.");
            return Ok(story);
        }
    }
}
