using System.Net.Http.Json;

namespace GatewayApi.Auth;

public class CoreAuthApiClient
{
    private readonly HttpClient _httpClient;

    public CoreAuthApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthProfileDto?> GetProfileAsync(string login, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<AuthProfileDto>(
            $"AuthProfile/GetProfile?login={Uri.EscapeDataString(login)}",
            cancellationToken);
    }
}