namespace CineFansApp.Application
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "Operación exitosa")
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ServiceResult<T> Error(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}
