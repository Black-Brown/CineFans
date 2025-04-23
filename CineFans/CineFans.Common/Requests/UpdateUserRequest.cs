namespace CineFans.Common.Requests
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
