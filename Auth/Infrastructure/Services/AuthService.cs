using Application.Interfaces;
using Domain.ConfigModels;
using Domain.Requests;
using Domain.Responses;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly KeycloakOptions _config;
    private readonly HttpClient _httpClient = new();
    public AuthService(IOptions<KeycloakOptions> config)
    {
        _config = config.Value;
    }
    public Task<CustomDataResponse<LoginResponse>> Login(LoginRequest request)
    {
        // TODO: LoginRequest için validaasyon eklenecek


        return null;

    }

    public async Task<TokenResponse> Refresh(RefreshRequest request)
    {
        var tokenEndpoint = _config.Endpoints.Token;

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "Bearer"),
            new KeyValuePair<string, string>("refresh_token", request.RefreshToken)
        });

        var response = await _httpClient.PostAsync(tokenEndpoint, requestData);


        // TODO: Exception handle edilecek
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not refresh token");
        }

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

        return tokenResponse;
    }
}
