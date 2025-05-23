﻿using Microsoft.AspNetCore.Mvc;
using EvoStory.BackendAPI.DTO;
using Evostory.Story.Models;
using System.Net.Mime;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Cors;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowedOrigins")]
    public class ChoiceController(IChoiceService choiceService) : ControllerBase
    {
        /// <summary>
        /// Creates choice.
        /// </summary>
        /// <param name="choice"></param>
        /// <response code="201">The Choice was succesfully created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPut(Name = nameof(CreateChoice))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateChoice(CreateChoiceDTO choice)
        {
            ChoiceDTO result;
            try
            {
                result = choiceService.CreateChoice(choice);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return BadRequest();
            }

            return Created($"api/Choice/{result.Id}", result);
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

            return Ok(result);
        }

        /// <summary>
        /// Get all Choices.
        /// </summary>
        /// <response code="200">The Choices were succesfully retrieved.</response>
        [HttpGet(Name = nameof(GetChoices))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ChoiceDTO>), StatusCodes.Status200OK)]
        public ActionResult GetChoices()
        {
            IEnumerable<ChoiceDTO> result;
            result = choiceService.GetChoices();
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
            catch (ArgumentNullException ex) //Remove try catch?
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

