using BlogDomain.Requests;
using BlogDomain.Responses;
using Domain.Responses;

namespace Application.Interfaces;

public interface IBlogService
{
    public Task<CustomDataResponse<List<BlogIndexResponse>>> GetAll(); // TODO: Filtreler eklenecek

    public Task<CustomDataResponse<BlogShowResponse>> GetById(int id);

    public Task<CustomDataResponse<BlogCreatedResponse>> Create(BlogCreateRequest request);

    public Task<CustomDataResponse<SuccessDataResponse>> Update(BlogUpdateRequest request);

    public Task<CustomDataResponse<SuccessDataResponse>> Delete(int id);

}
