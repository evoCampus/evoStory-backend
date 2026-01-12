using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }
        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("Nem található érvényes felhasználói ID.");
        }


        /// <summary>
        /// Can create a new Item.(name, description, ?Stackable, storyId)
        /// </summary>
        [HttpPost("createItem")]
        public async Task<ActionResult<ItemDTO>> CreateItem([FromBody] CreateItemDTO dto)
        {
            var createdItem = await _service.CreateItemAsync(dto);
            return Ok(createdItem);
        }


        /// <summary>
        /// Gives all the items by storyId.
        /// </summary>
        [HttpGet("story/{storyId}/allItems")]
        public async Task<ActionResult<List<ItemDTO>>> GetItemsByStory(Guid storyId)
        {
            var items = await _service.GetItemsByStoryIdAsync(storyId);
            return Ok(items);
        }


        /// <summary>
        /// Player can pickup an item from a story and it adds to the players inventory.
        /// </summary>
        [HttpPost("pickupItem")]
        public async Task<IActionResult> AddItemToInventory([FromBody] AddToInventoryDTO dto)
        {
            dto.SessionId = GetCurrentUserId();

            await _service.AddItemToInventoryAsync(dto);
            return Ok(new { message = "Sikeresen felvetted a tárgyat!" });
        }

        /// <summary>
        /// Gives you whats in your inventory.
        /// </summary>
        [HttpGet("my-inventory")]
        public async Task<ActionResult<List<InventoryItemDTO>>> GetInventory()
        {
            var currentUserId = GetCurrentUserId();

            var inventory = await _service.GetInventoryBySessionIdAsync(currentUserId);
            return Ok(inventory);
        }
    }
}