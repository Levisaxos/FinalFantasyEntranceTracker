namespace finalfantasyentrancetracker.api.Helper
{
    public class LogHelper
    {
        private static LogHelper _instance;
        public static LogHelper Instance { get { return _instance ?? (_instance = new LogHelper()); } }
        public LogHelper()
        {
            LogRecords = new List<string>();
        }
        private List<string> LogRecords { get; set; }


        public void Report(string text)
        {
            LogRecords.Add(text);
        }

        public string GetLogReport()
        {
            var rValue  =string.Join("\r\n", LogRecords);
            LogRecords.Clear();
            return rValue;
        }
    }
}
