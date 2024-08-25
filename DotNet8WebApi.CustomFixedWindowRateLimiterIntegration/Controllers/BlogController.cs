namespace DotNet8WebApi.CustomFixedWindowRateLimiterIntegration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public BlogController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        HttpResponseMessage response = await _httpClient.PostAsync(
            "/api/RateLimiting/fixed-window",
            null
        );
        var responseJson = await response.Content.ReadAsStringAsync();
        var statusCode = response.StatusCode;

        if (statusCode == HttpStatusCode.TooManyRequests)
        {
            return StatusCode(429, responseJson);
        }

        return Ok();
    }
}
