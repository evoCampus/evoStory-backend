using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Cors;
using EvoStory.BackendAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Evostory.Story.Models;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowedOrigins")]
    public class ImageController(IImageService imageService, ILogger<ImageController> logger) : ControllerBase
    {
        /// <summary>
        /// Get Base64 string of Image by Id
        /// </summary>
        /// <param name="imageId"></param>
        /// <response code="200">The Image was succesfully retrieved.</response>
        /// <response code="404">Image not found.</response>
        [HttpGet("{imageId}", Name = nameof(GetImage))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ImageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetImage(Guid imageId)
        {
            logger.LogInformation($"Getting image with Id: {imageId}.");
            try
            {
                var result = imageService.GetImage(imageId);
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                logger.LogWarning($"Image with Id: {imageId} was not found.");
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get all Images.
        /// </summary>
        /// <response code="200">The Images were succesfully retrieved.</response>
        [HttpGet(Name = nameof(GetAllImages))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ImageDTO>), StatusCodes.Status200OK)]
        public ActionResult GetAllImages()
        {
            logger.LogInformation("Getting all the images.");
            IEnumerable<ImageDTO> result;
            result = imageService.GetAllImages();
            return Ok(result);
        }
    }
}
