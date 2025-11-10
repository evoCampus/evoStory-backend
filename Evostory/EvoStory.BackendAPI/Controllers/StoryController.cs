using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Services;
using EvoStory.BackendAPI.Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using EvoStory.Database.Models;

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
        /// <response code="201">The Story was successfully created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPut(Name = nameof(CreateStory))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateStory(CreateStoryDTO story)
        {
            logger.LogInformation($"Creating story with Title: {story.Title};");
            StoryDTO result;
            try
            {
                result = storyService.CreateStory(story);
            }
            catch (RepositoryException ex)
            {
                logger.LogError(ex, "An error occurred when creating the story.");
                return BadRequest(ex.Message);
            }

            logger.LogInformation($"Story was created successfully with Title: {story.Title};");
            return Created($"api/Story/{result.Id}", result);
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
            try
            {
                var result = storyService.GetStory(storyId);
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                logger.LogError($"Story with Id: {storyId} not found.");
                return NotFound(ex.Message);
            }
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
            IEnumerable<StoryDTO> result;
            result = storyService.GetStories();
            return Ok(result);
        }

        /// <summary>
        /// Deletes a Story by Id.
        /// </summary>
        /// <param name="storyId"></param>
        /// <response code="200">The Story was successfully deleted.</response>
        /// <response code="404">Story not found.</response>
        [HttpDelete("{storyId}", Name = nameof(DeleteStory))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(StoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStory(Guid storyId)
        {
            logger.LogInformation($"Deleting story with Id: {storyId}.");
            StoryDTO result;
            try
            {
                result = storyService.DeleteStory(storyId);
            }
            catch (RepositoryException ex)
            {
                logger.LogError($"Story with Id: {storyId} not found.");
                return NotFound(ex.Message);
            }

            logger.LogInformation($"Story with Id: {storyId} was deleted.");
            return Ok(result);
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
            catch (RepositoryException ex)
            {
                logger.LogError($"Story with Id: {storyId} not found.");
                return NotFound(ex.Message);
            }

            logger.LogInformation($"Story with Id: {storyId} was edited.");
            return Ok(story);
        }
    }
}
