namespace CineFans.Common.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public BaseResponse() { }

        public BaseResponse(T data)
        {
            Success = true;
            Data = data;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
