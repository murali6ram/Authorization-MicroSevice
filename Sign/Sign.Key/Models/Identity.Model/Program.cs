// UNCOMMENT THE BELOW CODE ONLY FOR MIGRATIONS
// CHANGE Project Properties >> Application >> General >> Output Type to Console Application
using IdentityModel.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IdentityContext>((serviceProvider, options) =>
{
    string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=ram-ids;MinPoolSize=5;MaxPoolSize=100;ConnectionLifetime=0;";
    options.UseNpgsql(connectionString);
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();