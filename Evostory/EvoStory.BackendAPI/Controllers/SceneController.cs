using EvoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SceneController : ControllerBase
    {
        public static List<Scene> scenes = new();
        /// <summary>
        /// Creates a Scene.
        /// </summary>
        /// <param name="scene"></param>
        /// <response code="200">The Scene was successfully created.</response>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SceneDTO), StatusCodes.Status200OK)]
        public ActionResult CreateScene(CreateSceneDTO scene)
        {
            var newScene = new Scene
            {
                Id = Guid.NewGuid(),
                Content = new Content
                {
                    Text = scene.Content.Text,
                    ImageId = scene.Content.ImageId,
                    SoundId = scene.Content.SoundId
                },
                Choices = scene.Choices.Select(choiceDTO => new Choice()
                {
                    ChoiceText = choiceDTO.ChoiceText,
                    Id = Guid.NewGuid(),
                    NextSceneId = choiceDTO.NextSceneId
                }).ToList()
            };
            scenes.Add(newScene);
            return Created();
        }

        /// <summary>
        /// Get Scene by Id.
        /// </summary>
        /// <param name="sceneId"></param>
        /// <response code="200">The Scene was successfully retrieved.</response>
        /// <response code="400">Scene not found.</response>
        [HttpGet("{sceneId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SceneDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetScene(Guid sceneId)
        {
            var result = scenes.FirstOrDefault(scene => scene.Id == sceneId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Get all Scenes.
        /// </summary>
        /// <response code="200">The Scenes were successfully retrieved.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<SceneDTO>), StatusCodes.Status200OK)]
        public ActionResult GetScenes()
        {
            return Ok(scenes);
        }

        /// <summary>
        /// Deletes a Scene by Id.
        /// </summary>
        /// <param name="sceneId"></param>
        /// <response code="200">The Scene was successfully deleted.</response>
        /// <response code="400">Scene not found.</response>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteScene(Guid sceneId)
        {
            var result = scenes.FirstOrDefault(scene => scene.Id == sceneId);
            if (result == null)
            {
                return NotFound();
            }
            scenes.Remove(result);
            return NoContent();
        }
    }
}
