using Domain.Requests;
using Domain.Responses;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<CustomDataResponse<LoginResponse>> Login(LoginRequest request);

    Task<CustomDataResponse<TokenResponse>> Refresh(RefreshRequest request);

    Task<CustomApiResponse> Logout(LogoutRequest request);
    Task<CustomApiResponse> Register(RegisterRequest request);
}
