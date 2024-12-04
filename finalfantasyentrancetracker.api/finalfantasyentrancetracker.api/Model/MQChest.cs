using finalfantasyentrancetracker.api.Data;

namespace finalfantasyentrancetracker.api.Model
{
    [SqlTable("master", "Chest")]
    public class MQChest : MQBase
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
