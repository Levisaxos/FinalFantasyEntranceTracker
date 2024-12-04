using finalfantasyentrancetracker.api.Data;

namespace finalfantasyentrancetracker.api.Model
{
    [SqlTable("master", "Section")]
    public class MQSection : MQBase
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
