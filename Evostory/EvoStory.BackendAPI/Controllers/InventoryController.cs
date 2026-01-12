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
            var userIdClaim = User.FindFirst("UserId"); // A "UserId" kulcsot keressük
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("Nem található érvényes felhasználói ID.");
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem([FromBody] CreateItemDTO dto)
        {
            var createdItem = await _service.CreateItemAsync(dto);
            return Ok(createdItem);
        }

        [HttpGet("story/{storyId}")]
        public async Task<ActionResult<List<ItemDTO>>> GetItemsByStory(Guid storyId)
        {
            var items = await _service.GetItemsByStoryIdAsync(storyId);
            return Ok(items);
        }

        [HttpPost("pickup")]
        public async Task<IActionResult> AddItemToInventory([FromBody] AddToInventoryDTO dto)
        {
            dto.SessionId = GetCurrentUserId();

            await _service.AddItemToInventoryAsync(dto);
            return Ok(new { message = "Sikeresen felvetted a tárgyat!" });
        }

        [HttpGet("session")]
        public async Task<ActionResult<List<InventoryItemDTO>>> GetInventory(Guid sessionId)
        {
            var currentUserId = GetCurrentUserId();

            var inventory = await _service.GetInventoryBySessionIdAsync(sessionId);
            return Ok(inventory);
        }
    }
}