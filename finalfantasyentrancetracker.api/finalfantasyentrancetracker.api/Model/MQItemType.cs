using finalfantasyentrancetracker.api.Data;

namespace finalfantasyentrancetracker.api.Model
{
    [SqlTable("master", "ItemType")]
    public class MQItemType : MQBase
    {
        public MQItemType(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
