using Microsoft.AspNetCore.Mvc;
using EvoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using System.Net.Mime;
using EvoStory.BackendAPI.DTO;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoiceController : ControllerBase
    {
        public static List<Choice> choices = new();
        /// <summary>
        /// Creates choice.
        /// </summary>
        /// <param name="choice"></param>
        /// <response code="204">The Choice was succesfully created.</response>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ChoiceDTO), StatusCodes.Status204NoContent)]
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

        /// <summary>
        /// Get Choice by Id
        /// </summary>
        /// <param name="choiceId"></param>
        /// <response code="200">The Choice was succesfully retrieved.</response>
        /// <response code="404">Choice not found.</response>
        [HttpGet("{choiceId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ChoiceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetChoice(Guid choiceId)
        {
            var result = choices.FirstOrDefault(choice => choice.Id == choiceId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Get all Choices.
        /// </summary>
        /// <response code="200">The Choices were succesfully retrieved.</response>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<CreateChoiceDTO>), StatusCodes.Status200OK)]
        public ActionResult GetChoices()
        {
            return Ok(choices);
        }

        /// <summary>
        /// Deletes a Choice by Id.
        /// </summary>
        /// <param name="choiceId"></param>
        /// <response code="204">The Choice was succesfully deleted.</response>
        /// <response code="404">Choice not found.</response>
        [HttpDelete("{choiceId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

