using Application.Interfaces;
using BlogDomain.Requests;
using BlogDomain.Responses;
using Domain.Responses;

namespace Infrastructure.Services;

public class BlogService : IBlogService
{
    public Task<CustomDataResponse<BlogCreatedResponse>> Create(BlogCreateRequest request)
    {
        throw new NotImplementedException();
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
