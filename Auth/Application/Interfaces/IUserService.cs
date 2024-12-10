using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<CustomDataResponse<UserShowResponse>> GetAsync(string username);

    public Task<CustomApiResponse> CreateAsync(UserCreateRequest request);

    public CustomApiResponse Update(UserUpdateRequest request);

    public Task<CustomApiResponse> DeleteAsync(string username);
}
