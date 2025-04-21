using EvoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SceneController(ISceneService sceneService) : ControllerBase
    {
        public static List<Scene> scenes = new();
        /// <summary>
        /// Creates a Scene.
        /// </summary>
        /// <param name="scene"></param>
        /// <response code="204">The Scene was successfully created.</response>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SceneDTO), StatusCodes.Status204NoContent)]
        public ActionResult CreateScene(CreateSceneDTO scene)
        {
            //var newScene = new Scene
            //{
            //    Id = Guid.NewGuid(),
            //    Content = new Content
            //    {
            //        Text = scene.Content.Text,
            //        ImageId = scene.Content.ImageId,
            //        SoundId = scene.Content.SoundId
            //    },
            //    Choices = scene.Choices.Select(choiceDTO => new Choice()
            //    {
            //        ChoiceText = choiceDTO.ChoiceText,
            //        Id = Guid.NewGuid(),
            //        NextSceneId = choiceDTO.NextSceneId
            //    }).ToList()
            //};
            //scenes.Add(newScene);
            try
            {
                sceneService.CreateScene(scene);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Created();
        }

        /// <summary>
        /// Get Scene by Id.
        /// </summary>
        /// <param name="sceneId"></param>
        /// <response code="200">The Scene was successfully retrieved.</response>
        /// <response code="404">Scene not found.</response>
        [HttpGet("{sceneId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SceneDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetScene(Guid sceneId)
        {
            //var result = scenes.FirstOrDefault(scene => scene.Id == sceneId);
            var result = sceneService.GetScene(sceneId);
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
            var result = sceneService.GetScenes();
            return Ok(result);
        }

        /// <summary>
        /// Deletes a Scene by Id.
        /// </summary>
        /// <param name="sceneId"></param>
        /// <response code="204">The Scene was successfully deleted.</response>
        /// <response code="404">Scene not found.</response>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteScene(Guid sceneId)
        {
            //var result = scenes.FirstOrDefault(scene => scene.Id == sceneId);
            //if (result == null)
            //{
            //    return NotFound();
            //}
            //scenes.Remove(result);
            try
            {
                sceneService.DeleteScene(sceneId);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
