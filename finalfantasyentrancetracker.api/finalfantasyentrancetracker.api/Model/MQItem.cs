
using finalfantasyentrancetracker.api.Data;

namespace finalfantasyentrancetracker.api.Model
{
    [SqlTable("master", "Item")]
    public class MQItem : MQBase
    {
        public MQItem()
        {

        }
        public MQItem(string name, string imageName, string itemType, int itemRow, int sequence)
        {
            Name = name;
            ImagePath = imageName;
            ItemType = itemType;
            Sequence = sequence;
            ItemRow = itemRow;
        }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey(TargetTable = "MQItemType", TargetColumn = "Id")]
        public int ItemTypeId { get; set; }

        public int ItemRow { get; set; }
        public int Sequence { get; set; }

        [SqlColumn(Ignore = true)]
        public string ItemType { get; set; }
    }
}
