namespace DotNet8WebApi.CustomFixedWindowRateLimiterIntegration.Services;

public class HttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> ExecuteAsync<T>(
        string endpoint,
        EnumStatusCode statusCode,
        object? requestModel = null
    )
    {
        HttpResponseMessage? response = null;
        HttpContent? content = null;

        if (requestModel is not null)
        {
            string jsonStr = JsonConvert.SerializeObject(requestModel);
            content = new StringContent(jsonStr);
        }

        switch (statusCode)
        {
            case EnumStatusCode.GET:
                response = await _httpClient.GetAsync(endpoint);
                break;
            case EnumStatusCode.POST:
                response = await _httpClient.PostAsync(endpoint, content);
                break;
            case EnumStatusCode.PUT:
                response = await _httpClient.PutAsync(endpoint, content);
                break;
            case EnumStatusCode.PATCH:
                response = await _httpClient.PatchAsync(endpoint, content);
                break;
            case EnumStatusCode.DELETE:
                response = await _httpClient.DeleteAsync(endpoint);
                break;
            case EnumStatusCode.None:
            default:
                break;
        }

        var responseJson = await response!.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseJson)!;
    }
}
