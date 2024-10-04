using Application.Extensions;
using Application.Interfaces;
using Domain.ConfigModels;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Mapping;
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
    public async Task<CustomDataResponse<LoginResponse>> Login(LoginRequest request)
    {
        var tokenEndpoint = _config.Endpoints.Token;

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", request.Username),
        });

        var response =await _httpClient.PostAsync(tokenEndpoint, requestData); 


        // TODO: Exception handle edilecek
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not login");
        }

        var content = response.Content.ReadAsStringAsync();

        var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content.Result);


        return loginResponse.ToCustomDataResponse(true);

    }

    public async Task<CustomApiResponse> Logout(LogoutRequest request)
    {
        var tokenEndpoint = _config.Endpoints.Logout;

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),    
            new KeyValuePair<string, string>("refresh_token", request.RefreshToken),
        });

        var response =await _httpClient.PostAsync(tokenEndpoint, requestData);

        // TODO: Exception handle edilecek
        if (!response.IsSuccessStatusCode)

        {
            throw new Exception("Could not logout");
        }

        return CommonResponseMapper.ToCustomApiResponse(true);
    }
    
    public async Task<CustomApiResponse> Register(RegisterRequest request)
    {
        var adminToken = await GetAdminAccessTokenAsync();

        var registerEndpoint = _config.Endpoints.Register;
        
        var requestData = new StringContent(JsonSerializer.Serialize( new
        {
            username = request.Username,
            email = request.Email,
            enabled = true,
            credentials = new[]
            {
                new
                {
                    type = "password",
                    value = request.Password,
                    temporary = false
                }
            }
        }), System.Text.Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminToken);

        var response = await _httpClient.PostAsync(registerEndpoint, requestData);

        // TODO: Exception handle edilecek
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not register user");
        }
        
        // TODO: add user to the database

        return CommonResponseMapper.ToCustomApiResponse(true);
    }

    public async Task<CustomDataResponse<TokenResponse>> Refresh(RefreshRequest request)
    {
        var tokenEndpoint = _config.Endpoints.Token;

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
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

        return tokenResponse.ToCustomDataResponse(true);
    }
    
    private async Task<string> GetAdminAccessTokenAsync()
    {
        var tokenEndpoint = _config.Endpoints.Token;

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        var response = await _httpClient.PostAsync(tokenEndpoint, requestData);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not retrieve admin access token");
        }

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

        return tokenResponse.Token;
    }

    
}
