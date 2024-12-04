namespace finalfantasyentrancetracker.api.Data
{
    public class ForeignKeyAttribute : Attribute
    {
        public string TargetTable { get; set; }
        public string TargetColumn { get; set; }
    }
}
