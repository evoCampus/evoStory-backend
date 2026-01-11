namespace EvoStory.BackendAPI.DTO
{
    public class InventoryItemDTO
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string? ItemDescription { get; set; }
        public int Quantity { get; set; }
    }
}