using DotNet8WebApi.CustomFixedWindowRateLimiterIntegration.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(n =>
{
    return new HttpClient() { BaseAddress = new Uri("https://localhost:7218") };
});

builder.Services.AddScoped<HttpClientService>();
builder.Services.AddScoped<FixedWindowRateLimiterService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
