using AutoMapper;
using CineFans.Common.Dtos;
using CineFans.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ReverseMap();
    }
}
