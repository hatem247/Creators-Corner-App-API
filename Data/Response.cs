namespace Creators_Corner_App_API.Data
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static Response<T> Success(string message, T data)
        {
            return new Response<T>
            {
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static Response<T> Fail(string message)
        {
            return new Response<T>
            {
                Status = false,
                Message = message,
                Data = default
            };
        }
    }
}
