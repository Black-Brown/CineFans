namespace CinesFansApp.API.Models
{
    public class ApiResponse<T> // ¡Falta el parámetro genérico <T>!
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; } // Añade "?" si T puede ser null

        public static ApiResponse<T> Ok(T data, string message = "Operación exitosa")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default // o Data = null
            };
        }
    }
}