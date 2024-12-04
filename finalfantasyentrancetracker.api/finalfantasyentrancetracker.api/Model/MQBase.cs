using finalfantasyentrancetracker.api.Data;

namespace finalfantasyentrancetracker.api.Model
{
    public class MQBase
    {
        [SqlColumn(First = true, Identity = true, PrimaryKey = true)]
        public int Id { get; set; }
        [SqlColumn(Last = true)]
        public DateTime CreatedOn { get; set; }
        [SqlColumn(Last = true)]
        public DateTime UpdatedOn { get; set; }
    }
}
