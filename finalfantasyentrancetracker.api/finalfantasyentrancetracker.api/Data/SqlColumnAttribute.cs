namespace finalfantasyentrancetracker.api.Data
{
    public class SqlColumnAttribute : Attribute
    {        
        public bool First{ get; set; }
        public bool Last { get; set; }
        public bool Identity { get; set; }
        public bool PrimaryKey{ get; set; }

        public bool Ignore { get; set; }
    }
}
