using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Exceptions;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [EnableCors("allowedOrigins")]
    public class SceneController(ISceneService sceneService, ILogger<SceneController> logger) : ControllerBase
    {
        /// <summary>
        /// Creates a Scene.
        /// </summary>
        /// <param name="scene"></param>
        /// <response code="201">The Scene was successfully created.</response>
        /// <response code="400">The Scene was not created.</response>
        [HttpPut(Name = nameof(CreateScene))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SceneDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateScene(CreateSceneDTO scene)
        {
            logger.LogInformation("Create scene endpoint was called.");
            SceneDTO result;
            try
            {
                result = sceneService.CreateScene(scene);
            }
            catch (RepositoryException ex)
            {
                logger.LogError(ex, "An error occurred when creating the scene.");
                return BadRequest(ex.Message);
            }

            logger.LogInformation($"Scene was created successfully with Id: {result.Id}");
            return Created($"api/Scene/{result.Id}", result);
        }

        /// <summary>
        /// Get Scene by Id.
        /// </summary>
        /// <param name="sceneId"></param>
        /// <response code="200">The Scene was successfully retrieved.</response>
        /// <response code="404">Scene not found.</response>
        [HttpGet("{sceneId}", Name = nameof(GetScene))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SceneDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetScene(Guid sceneId)
        {
            logger.LogInformation($"Getting scene with Id: {sceneId}.");
            try
            {
                var result = sceneService.GetScene(sceneId);
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                logger.LogError($"No scene with Id: {sceneId} found.");
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get all Scenes.
        /// </summary>
        /// <response code="200">The Scenes were successfully retrieved.</response>
        [HttpGet(Name = nameof(GetScenes))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<SceneDTO>), StatusCodes.Status200OK)]
        public ActionResult GetScenes()
        {
            logger.LogInformation("Getting all scenes.");
            IEnumerable<SceneDTO> result;
            result = sceneService.GetScenes();
            return Ok(result);
        }

        /// <summary>
        /// Deletes a Scene by Id.
        /// </summary>
        /// <param name="sceneId"></param>
        /// <response code="200">The Scene was successfully deleted.</response>
        /// <response code="404">Scene not found.</response>
        [HttpDelete(Name = nameof(DeleteScene))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SceneDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteScene(Guid sceneId)
        {
            logger.LogInformation($"Deleting scene with Id: {sceneId}.");
            SceneDTO result;
            try
            {
                result = sceneService.DeleteScene(sceneId);
            }
            catch (RepositoryException ex)
            {
                logger.LogError(ex, "The scene was not found.");
                return NotFound(ex.Message);
            }

            logger.LogInformation($"Scene with Id: {sceneId} was deleted.");
            return Ok(result);
        }
    }
}
