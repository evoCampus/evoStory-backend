using EvoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SceneController : ControllerBase
    {
        public static List<Scene> scenes = new();
        [HttpPut]
        public ActionResult CreateScene(CreateSceneDTO scene)
        {
            var newScene = new Scene
            {
                Id = Guid.NewGuid(),
                Content = scene.Content,
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
