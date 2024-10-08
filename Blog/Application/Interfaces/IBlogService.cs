
using BlogDomain.Requests;
using BlogDomain.Responses;
using Domain.Responses;

namespace Application.Interfaces;

public interface IBlogService
{
    public Task<CustomDataResponse<BlogCreatedResponse>> Create(BlogCreateRequest request);
}
