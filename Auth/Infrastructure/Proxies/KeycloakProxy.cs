using Application.Interfaces;
using Azure;
using Azure.Core;
using Domain.ConfigModels;
using Domain.Requests;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Transactions;

namespace Infrastructure.Proxies;

public class KeycloakProxy : IKeycloakProxy
{
    private readonly HttpClient _httpClient = new();
    private readonly ILogger<AuthService> _logger;
    private readonly KeycloakOptions _config;

    public KeycloakProxy(ILogger<AuthService> logger)
    {
        _logger = logger;
    }

    public async Task<LoginResponse> LoginAsync(FormUrlEncodedContent requestData)
    {
        try
        {
            var tokenEndpoint = GenerateFullUrl(_config.Endpoints.Token);

            var response = await _httpClient.PostAsync(tokenEndpoint, requestData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Keycloak API request failed. StatusCode: {response.StatusCode}, Response: {errorContent}");
            }


            var content = await response.Content.ReadAsStringAsync();

            if (content == null)
            {
                throw new HttpRequestException($"Keycloak API request failed. Content must not be null. StatusCode: {response.StatusCode}");
            }

            return JsonSerializer.Deserialize<LoginResponse>(content);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while communicating with Keycloak.");
            throw new HttpRequestException($"Keycloak API request failed. StatusCode: {ex.StatusCode}, Response: {ex}");
        }
        catch (Exception ex)
        {
            // Genel istisna yakalama (gerekli durumlarda)
            _logger.LogError(ex, "Unexpected error occurred.");
            throw new TransactionException($"Unexpected error occurred. Response: {ex}");
        }
    }

    public async Task LogoutAsync(FormUrlEncodedContent requestData)
    {
        try
        {
            var tokenEndpoint = GenerateFullUrl(_config.Endpoints.Logout);

            var response = await _httpClient.PostAsync(tokenEndpoint, requestData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Keycloak API request failed during logout. StatusCode: {response.StatusCode}, Response: {errorContent}");
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while communicating with Keycloak during logout.");
            throw new HttpRequestException($"Keycloak API logout failed. StatusCode: {ex.StatusCode}, Response: {ex}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred during logout.");
            throw new TransactionException($"Unexpected error occurred during logout. Response: {ex}");
        }
    }

    public async Task RegisterAsync(StringContent? requestData)
    {
        try
        {
            var adminToken = await GetAdminAccessTokenAsync();
            var registerEndpoint = GenerateFullUrl(_config.Endpoints.Register);

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminToken);

            var response = await _httpClient.PostAsync(registerEndpoint, requestData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error during registration. StatusCode: {response.StatusCode}, Response: {errorContent}");
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while communicating with Keycloak during registration.");
            throw new HttpRequestException($"Keycloak API registration failed. Response: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred during registration.");
            throw new TransactionException($"Unexpected error occurred during registration. Response: {ex.Message}", ex);
        }
    }

    public async Task<TokenResponse> RefreshAsync(FormUrlEncodedContent requestData)
    {
        try
        {
            var tokenEndpoint = GenerateFullUrl(_config.Endpoints.Token);

            var response = await _httpClient.PostAsync(tokenEndpoint, requestData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Token refresh failed. StatusCode: {response.StatusCode}, Response: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

            if (tokenResponse == null)
            {
                throw new HttpRequestException("Failed to deserialize TokenResponse. The response content is null.");
            }

            return tokenResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "An error occurred while refreshing the token.");
            throw new HttpRequestException($"Token refresh failed. Response: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred during token refresh.");
            throw new TransactionException($"Unexpected error occurred during token refresh. Response: {ex.Message}", ex);
        }
    }


    private async Task<string> GetAdminAccessTokenAsync()
    {
        var tokenEndpoint = GenerateFullUrl(_config.Endpoints.Token);

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _config.ClientId),
            new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        var response = await _httpClient.PostAsync(tokenEndpoint, requestData);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ToString());
        }

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

        return tokenResponse.AccessToken;
    }


    private string GenerateFullUrl(string url)
    {
        return _config.KeyCloakBaseUrl + url;
    }


}
