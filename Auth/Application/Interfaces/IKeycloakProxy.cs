using Domain.Responses;

namespace Application.Interfaces;

public interface IKeycloakProxy
{
    Task<LoginResponse> LoginAsync(FormUrlEncodedContent requestData);
    Task LogoutAsync(FormUrlEncodedContent requestData);
    Task RegisterAsync(StringContent? requestData);
    Task<TokenResponse> RefreshAsync(FormUrlEncodedContent request);
}
