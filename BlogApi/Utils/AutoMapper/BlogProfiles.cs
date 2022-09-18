using AutoMapper;
using BlogApi.Data.Entities;
using BlogApi.Dtos;

namespace BlogApi.Utils.AutoMapper
{
    public class BlogProfiles : Profile
    {
        public BlogProfiles()
        {
            CreateMap<BlogPost, ReadPostDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss")));

            CreateMap<CreatePostDto, BlogPost>();

            CreateMap<UpdatePostDto, BlogPost>();

            CreateMap<User, ReadUserDto>();

            CreateMap<RegisterUserDto, User>();
        }
    }
}
