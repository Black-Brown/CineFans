namespace CineFans.Common.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
    }
}
