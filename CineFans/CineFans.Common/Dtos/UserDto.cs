namespace CineFans.Common.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
