namespace finalfantasyentrancetracker.api.Data
{
    public class SqlTableAttribute : Attribute
    {
        public SqlTableAttribute(string schema, string name)
        {
            Schema = schema;
            Name = name;

        }
        public string Schema { get; set; }
        public string Name { get; set; }
    }
}
