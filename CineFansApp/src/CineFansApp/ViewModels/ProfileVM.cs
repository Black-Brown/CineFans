using CineFansApp.Domain.DTOs;
namespace CineFansApp.Frontend.ViewModels
{
    public class ProfileVM
    {
        public UserDto User { get; set; }
        public List<PostDto> Posts { get; set; }
        public bool IsCurrentUser { get; set; }
        public bool IsFollowing { get; set; }
    }
}
