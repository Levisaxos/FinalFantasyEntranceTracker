using finalfantasyentrancetracker.api.Data;

namespace finalfantasyentrancetracker.api.Model
{
    [SqlTable("master", "Entrance")]
    public class MQEntrance : MQBase
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
