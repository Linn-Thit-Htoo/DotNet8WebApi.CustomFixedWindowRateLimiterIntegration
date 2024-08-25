using DotNet8WebApi.CustomFixedWindowRateLimiterIntegration.Services;

namespace DotNet8WebApi.CustomFixedWindowRateLimiterIntegration.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly FixedWindowRateLimiterService _fixedWindowRateLimiterService;

    public BlogController(FixedWindowRateLimiterService fixedWindowRateLimiterService)
    {
        _fixedWindowRateLimiterService = fixedWindowRateLimiterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        var response = await _fixedWindowRateLimiterService.ApplyFixedWindowRateLimiterAsync();
        var responseJson = await response.Content.ReadAsStringAsync();
        var statusCode = response.StatusCode;

        if (statusCode == HttpStatusCode.TooManyRequests)
        {
            return StatusCode(429, responseJson);
        }

        return Ok();
    }
}
