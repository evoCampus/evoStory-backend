using Microsoft.AspNetCore.Mvc;
using EvoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using System.Net.Mime;
using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Services;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoiceController(IChoiceService choiceService) : ControllerBase
    {
        public static List<Choice> choices = new();
        /// <summary>
        /// Creates choice.
        /// </summary>
        /// <param name="choice"></param>
        /// <response code="200">The Choice was succesfully created.</response>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ChoiceDTO), StatusCodes.Status200OK)]
        public ActionResult CreateChoice(CreateChoiceDTO choice)
        {
            try
            {
                choiceService.CreateChoice(choice);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Created();
        }

        /// <summary>
        /// Get Choice by Id
        /// </summary>
        /// <param name="choiceId"></param>
        /// <response code="200">The Choice was succesfully retrieved.</response>
        /// <response code="400">Choice not found.</response>
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
        /// <response code="200">The Choice was succesfully deleted.</response>
        /// <response code="400">Choice not found.</response>
        [HttpDelete("{choiceId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

