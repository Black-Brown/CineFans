
using System.Collections.Generic;
using CineFansApp.Domain.DTOs;

namespace CineFansApp.Frontend.ViewModels
{
    public class HomeVM
    {
        public List<PostDto> Posts { get; set; }
        public List<UserDto> UsuariosSugeridos { get; set; }
        public List<MovieDto> PeliculasPopulares { get; set; }
    }
}

