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
    public class SceneController(ISceneService sceneService) : ControllerBase
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
            SceneDTO result;
            try
            {
                result = sceneService.CreateScene(scene);
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex);
            }

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
            SceneDTO result;
            try
            {
                result = sceneService.GetScene(sceneId);
            }
            catch (RepositoryException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(result);
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
            SceneDTO result;
            try
            {
                result = sceneService.DeleteScene(sceneId);
            }
            catch (RepositoryException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(result);
        }
    }
}
