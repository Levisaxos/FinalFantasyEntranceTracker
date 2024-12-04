using finalfantasyentrancetracker.api.Data;

namespace finalfantasyentrancetracker.api.Model
{
    [SqlTable("master", "Map")]
    public class MQMap : MQBase
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
