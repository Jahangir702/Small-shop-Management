namespace MVC.HttpServices
{
    public class ResponseOutcome<T> where T : class
    {
        public ResponseStatus ResponseStatus { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public T Entity { get; set; }

        public List<T> EntityList { get; set; }

        public ResponseOutcome()
        {
            ResponseStatus = ResponseStatus.Failed;
            Message = string.Empty;

            Entity = null;
            EntityList = new List<T>();
        }
    }

    public enum ResponseStatus : byte
    {
        Failed = 0,
        Success = 1
    }
}