using evoStory.BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using evoStory.BackendAPI.DTO;

namespace evoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoiceController : ControllerBase
    {
        public static List<Choice> choices = new();

        [HttpPut]
        public ActionResult CreateChoice(CreateChoiceDTO choice)
        {
            var newChoice = new Choice
            {
                Id = Guid.NewGuid(),
                NextSceneId = choice.NextSceneId,
                ChoiceText = choice.ChoiceText
            };
            choices.Add(newChoice);
            return Created();
        }

        [HttpGet("{choiceId}")]
        public ActionResult GetChoice(Guid choiceId)
        {
            var result = choices.FirstOrDefault(choice => choice.Id == choiceId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public ActionResult GetChoices()
        {
            return Ok(choices);
        }

        [HttpDelete("{choiceId}")]
        public ActionResult DeleteChoice(Guid choiceId)
        {
            var result = choices.FirstOrDefault(choice => choice.Id == choiceId);
            if (result == null)
            {
                return NotFound();
            }
            choices.Remove(result);
            return NoContent();
        }
    }
}

