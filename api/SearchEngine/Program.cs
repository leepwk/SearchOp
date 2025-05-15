using SearchEngine.Common.Model;
using SearchEngine.Repository;
using SearchEngine.Repository.Helpers;
using SearchEngine.Repository.Interface;
using SearchEngine.Service;
using SearchEngine.Service.Interface;

// Please see the README.md for to explain this playwright browser installation
Console.WriteLine("Installing Playwright browsers...");
var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
if (exitCode != 0)
{
    Console.WriteLine("Browser installation failed.");
    Environment.Exit(exitCode);
}
Console.WriteLine("Browser installation completed.");


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Bind the configuration section to the MySettings class
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Read allowed origins from configuration
var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddPolicy("ConfiguredCORS", builder =>
    {
        builder
            .WithOrigins(allowedOrigins!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Add required DI instantiations
// Register IHttpClientFactory to use HttpClient
builder.Services.AddHttpClient();

builder.Services.AddTransient<IDataConnection, DataConnection>();
builder.Services.AddTransient<IWebScraper, WebScraper>();
builder.Services.AddTransient<ISearchRepository, SearchRepository>();
builder.Services.AddTransient<ISearchService, SearchService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Set CORS
app.UseCors("ConfiguredCORS");

app.MapControllers();

app.Run();
