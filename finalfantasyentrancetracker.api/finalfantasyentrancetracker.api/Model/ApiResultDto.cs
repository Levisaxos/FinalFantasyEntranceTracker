namespace finalfantasyentrancetracker.api.Model
{
    public class ApiResultDto<T>
    {
        public List<LogRecord> LogRecords { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string LogMessages { get; set; }
        public T Data { get; set; }
        public bool IsSuccessfull
        {
            get
            {
                return LogRecords == null || !LogRecords.Any(x => x.MessageType == api.Data.LogRecordType.Error);
            }
        }

        public ApiResultDto(T data)
        {
            if (data == null)
                return;

            ErrorMessages = new List<string>();
            if (data.GetType() == typeof(Exception))
                ErrorMessages.Add((data as Exception).Message);

            Data = data;
        }
    }
}
