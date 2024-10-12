using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWorks;
using BlogDomain.Entities;
using BlogDomain.Requests;
using BlogDomain.Responses;
using Domain.Responses;

namespace Infrastructure.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }
    public Task<CustomDataResponse<BlogCreatedResponse>> Create(BlogCreateRequest request)
    {
        Blog blog = new Blog();

        _blogRepository.Create(blog);

        
    }

    public Task<CustomDataResponse<SuccessDataResponse>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CustomDataResponse<List<BlogIndexResponse>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<CustomDataResponse<BlogShowResponse>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CustomDataResponse<SuccessDataResponse>> Update(BlogUpdateRequest request)
    {
        throw new NotImplementedException();
    }
}
