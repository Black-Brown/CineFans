using Microsoft.AspNetCore.Http;

namespace CineFans.Common.Requests
{
    public class UpdateMovieRequest : CreateMovieRequest
    {
        public int MovieId { get; set; }
    }
}
