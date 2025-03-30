using evoStory.BackendAPI.DTO;
using evoStory.BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace evoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SceneController : ControllerBase
    {
        public static List<Scene> scenes = new();

        [HttpPut]
        public ActionResult CreateScene(CreateSceneDTO story)
        {
            var newScene = new Scene
            {
                Id = Guid.NewGuid(),
                
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
