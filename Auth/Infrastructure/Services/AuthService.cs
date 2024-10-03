using Application.Interfaces;
using Domain.Requests;
using Domain.Responses;
using System.Net.Http;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    public Task<CustomDataResponse<LoginResponse>> Login(LoginRequest request)
    {
        // TODO: LoginRequest için validaasyon eklenecek


        return null;

    }

    public async Task<TokenResponse> Refresh(RefreshRequest request)
    {
        var tokenEndpoint = $"{_config["Keycloak:Authority"]}/protocol/openid-connect/token";

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config["Keycloak:ClientId"]),
            new KeyValuePair<string, string>("client_secret", _config["Keycloak:ClientSecret"]),
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", refreshToken)
        });

        var response = await _httpClient.PostAsync(tokenEndpoint, requestData);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not refresh token");
        }

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);

        return tokenResponse;
    }
}
