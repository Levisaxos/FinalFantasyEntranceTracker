namespace finalfantasyentrancetracker.api.Model.Api
{
    public class ItemRowCollectionDto
    {
        public int RowNumber { get; set; }
        public int Sequence { get; set; }
        public IEnumerable<ItemRowItemDto> Items { get; set; }
    }
}
