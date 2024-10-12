using AutoMapper;
using BlogDomain.Entities;
using BlogDomain.Requests;
using BlogDomain.Responses;

namespace Infrastructure.Mapping;

public class MapProfile : Profile
{

    public MapProfile()
    {
        CreateMap<BlogCreateRequest, Blog>();
        CreateMap<Blog, BlogCreatedResponse>();

        CreateMap<Blog, BlogIndexResponse>();

        CreateMap<Blog, BlogShowResponse>();

    }
}
