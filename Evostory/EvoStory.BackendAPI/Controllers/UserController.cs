using EvoStory.BackendAPI.DTO;
using EvoStory.Database.Exceptions;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace EvoStory.BackendAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowedOrigins")]
    public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
    {
        private const string USER_ID_CLAIM_NAME = "UserId";

        /// <summary>
        /// Creates user.
        /// </summary>
        /// <param name="user"></param>
        /// <response code="201">The User was successfully created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPut(Name = nameof(CreateUser))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO user)
        {
            logger.LogInformation("Create user endpoint was called.");
            try
            {
                var result = await userService.CreateUser(user);
                logger.LogInformation($"User was created successfully with Id: {result.Id}");
                return Created($"api/User/{result.Id}", result);
            }
            catch (RepositoryException ex)
            {
                logger.LogError(ex, "An error occurred when creating the scene.");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="200">The User was succesfully retrieved.</response>
        [HttpGet("{userId}", Name = nameof(GetUser))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUser(Guid userId)
        {
            logger.LogInformation($"Getting user with Id: {userId}.");
            try
            {
                var result = await userService.GetUser(userId);
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                logger.LogError($"No user with Id: {userId} found.");
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get all Users.
        /// </summary>
        /// <response code="200">The Users were successfully retrieved.</response>
        [HttpGet(Name = nameof(GetUsers))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetUsers()
        {
            logger.LogInformation("Getting all users.");
            IEnumerable<UserDTO> result;
            result = await userService.GetUsers();
            return Ok(result);
        }

        /// <summary>
        /// Deletes a User by Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="200">The User was successfully deleted.</response>
        /// <response code="404">User not found.</response>
        [HttpDelete(Name = nameof(DeleteUser))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SceneDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(Guid userId)
        {
            logger.LogInformation($"Deleting user with Id: {userId}.");
            try
            {
                var result = await userService.DeleteUser(userId);
                logger.LogInformation($"Scene with Id: {userId} was deleted.");
                return Ok(result);
            }
            catch (RepositoryException ex)
            {
                logger.LogError(ex, "The user was not found.");
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginDto">The username and password pair.</param>
        /// <response code="200">The user was successfully authenticated.</response>
        /// <response code="401">Invalid username or password.</response>
        [HttpPost("login", Name = nameof(Login))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login(LoginDTO loginDto)
        {
            logger.LogInformation($"Login attempt for user: {loginDto.UserName}");
            try
            {
                var user = await userService.Login(loginDto.UserName, loginDto.Password);
                return Ok(user);
            }
            catch (RepositoryException ex)
            {
                logger.LogError($"Login failed for user: {loginDto.UserName}. Reason: {ex.Message}");
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Logs out the user and clears the session cookie.
        /// </summary>
        /// <response code="200">The user was sucessfully logged out.</response>
        [HttpPost("logout", Name = nameof(Logout))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Logout()
        {
            logger.LogInformation("Logout endpoint called");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            logger.LogInformation("Logged out successfully");
            return Ok();
        }

        /// <summary>
        /// Gets the current authenticated user from session.
        /// </summary>
        /// <response code="200">The current User was successfully retrieved.</response>
        /// <response code="401">There is no user logged in.</response>
        [HttpGet("current", Name = nameof(GetCurrentUser))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(USER_ID_CLAIM_NAME);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Unauthorized("Invalid session");
            }

            try
            {
                var user = userService.GetUser(userId);
                return Ok(user);
            }
            catch (RepositoryException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
