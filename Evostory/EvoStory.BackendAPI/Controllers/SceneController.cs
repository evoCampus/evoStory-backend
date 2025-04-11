using evoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using Microsoft.AspNetCore.Mvc;

namespace evoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SceneController : ControllerBase
    {
        public static List<SceneDTO> scenes = new();
        [HttpPut]
        public ActionResult CreateScene(CreateSceneDTO scene)
        {
            var newScene = new SceneDTO
            {
                Id = Guid.NewGuid(),
                Content = scene.Content,
                Choices = scene.Choices.ToList(),
            };
            scenes.Add(newScene);
            return Created();
        }

        [HttpGet("{sceneId}")]
        public ActionResult GetScene(Guid sceneId)
        {
            var result = scenes.FirstOrDefault(scene => scene.Id == sceneId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public ActionResult GetScenes()
        {
            return Ok(scenes);
        }

        [HttpDelete]
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
