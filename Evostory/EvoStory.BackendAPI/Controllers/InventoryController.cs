using EvoStory.BackendAPI.DTO;
using EvoStory.BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EvoStory.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
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
            await _service.AddItemToInventoryAsync(dto);
            return Ok(new { message = "Sikeresen felvetted a tárgyat!" });
        }

        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<List<InventoryItemDTO>>> GetInventory(Guid sessionId)
        {
            var inventory = await _service.GetInventoryBySessionIdAsync(sessionId);
            return Ok(inventory);
        }
    }
}