using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<CustomDataResponse<LoginResponse>> Login(LoginRequest request);
}
