namespace CineFans.Common.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
