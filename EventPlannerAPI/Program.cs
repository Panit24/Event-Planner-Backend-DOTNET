using EventPlannerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure specific port
builder.WebHost.UseUrls("http://localhost:5036");

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// Add logging when server starts
app.Lifetime.ApplicationStarted.Register(() => {
    Console.WriteLine("Server is running on http://localhost:5036");
    // If you want to use built-in logging:
    app.Logger.LogInformation("Server is running on http://localhost:5036");
});

app.Run();
