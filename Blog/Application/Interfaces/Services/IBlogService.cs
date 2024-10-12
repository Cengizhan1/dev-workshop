using BlogDomain.Requests;
using BlogDomain.Responses;
using Domain.Responses;

namespace Application.Interfaces.Services;

public interface IBlogService
{
    public Task<CustomDataResponse<BlogIndexResponse>> GetAllAsync(); // TODO: Filtreler eklenecek

    public Task<CustomDataResponse<BlogShowResponse>> GetByIdAsync(int id);

    public Task<CustomDataResponse<BlogCreatedResponse>> CreateAsync(BlogCreateRequest request);

    public Task<CustomApiResponse> UpdateAsync(int id, BlogUpdateRequest request);

    public Task<CustomApiResponse> DeleteAsync(int id);

}
