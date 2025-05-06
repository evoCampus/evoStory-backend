using Microsoft.AspNetCore.Mvc;
using EvoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using System.Net.Mime;
using EvoStory.BackendAPI.Services;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoiceController(IChoiceService choiceService) : ControllerBase
    {
        /// <summary>
        /// Creates choice.
        /// </summary>
        /// <param name="choice"></param>
        /// <response code="204">The Choice was succesfully created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPut(Name = nameof(CreateChoice))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <response code="404">Choice not found.</response>
        [HttpGet("{choiceId}", Name = nameof(GetChoice))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ChoiceDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetChoice(Guid choiceId)
        {
            var result = choiceService.GetChoice(choiceId);
            if (result is null)
            {
                return NotFound();
            }

            var choiceDTO = new ChoiceDTO
            {
                Id = result.Id,
                NextSceneId = result.NextSceneId,
                ChoiceText = result.ChoiceText
            };

            return Ok(choiceDTO);
        }

        /// <summary>
        /// Get all Choices.
        /// </summary>
        /// <response code="200">The Choices were succesfully retrieved.</response>
        [HttpGet(Name = nameof(GetChoices))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<CreateChoiceDTO>), StatusCodes.Status200OK)]
        public ActionResult GetChoices()
        {
            var result = choiceService.GetChoices();
            return Ok(result);
        }

        /// <summary>
        /// Deletes a Choice by Id.
        /// </summary>
        /// <param name="choiceId"></param>
        /// <response code="204">The Choice was succesfully deleted.</response>
        /// <response code="404">Choice not found.</response>
        [HttpDelete("{choiceId}", Name = nameof(DeleteChoice))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteChoice(Guid choiceId)
        {
            try
            {
                choiceService.DeleteChoice(choiceId);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

