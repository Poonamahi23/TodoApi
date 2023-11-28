using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ms Copilot plugin",
        Version = "v1",
        Description = "Get the list of Weather Fore cast",
        Contact = new OpenApiContact
        {
            Email = "contact@WeatherForecast.com",
        },
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/.well-known/ai-plugin.json", () =>
{
    var aiPluginJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "ai-plugin.json");
    return File.Exists(aiPluginJsonPath)
        ? Results.File(aiPluginJsonPath, "application/json")
        : Results.NotFound();
});

app.MapGet("/logo.png", () =>
{
    var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "logo.png");
    return File.Exists(logoPath)
        ? Results.File(logoPath, "image/png")
        : Results.NotFound();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
