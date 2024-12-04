namespace finalfantasyentrancetracker.api.Model.Api
{
    public class ItemRowItemDto
    {
        public int Sequence { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
    }
}
