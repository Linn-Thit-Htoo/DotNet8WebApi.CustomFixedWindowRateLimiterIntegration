namespace DotNet8WebApi.CustomFixedWindowRateLimiterIntegration.Services;

public class FixedWindowRateLimiterService
{
    private readonly HttpClient _httpClient;

    public FixedWindowRateLimiterService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> ApplyFixedWindowRateLimiterAsync()
    {
        HttpResponseMessage response = await _httpClient.PostAsync(
            "/api/RateLimiting/fixed-window",
            null
        );
        return response;
    }
}
