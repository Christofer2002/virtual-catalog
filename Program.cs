using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VirtualCatalogAPI.Helpers;
using VirtualCatalogAPI.Businesses.Auth;
using VirtualCatalogAPI.Data.Repository.Auth;
using System.Diagnostics;
using VirtualCatalogAPI.Businesses.Categories;
using VirtualCatalogAPI.Businesses.Products;
using VirtualCatalogAPI.Businesses.Users;
using VirtualCatalogAPI.Data.Repository.Categories;
using VirtualCatalogAPI.Data.Repository.Products;
using VirtualCatalogAPI.Data.Repository.Users;
using VirtualCatalogAPI.Data;

var builder = WebApplication.CreateBuilder(args);

RunFlywayMigration();

// Add services to the container
builder.Services.AddControllers();

// Register ProductService and dependencies
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register CategoryService and dependencies
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Register UserService and dependencies
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register AuthService and dependencies
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// Register RoleService and dependencies
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


// Register JwtHelper
builder.Services.AddSingleton<JwtHelper>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new JwtHelper(
        configuration["Jwt:Key"],
        configuration["Jwt:Issuer"],
        configuration["Jwt:Audience"]
    );
});

// Configure JWT Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<DatabaseHelper>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Allow CORS
app.UseCors("AllowReactApp");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Add Authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void RunFlywayMigration()
{
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "run-flyway.bat",
            Arguments = "migrate",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        }
    };

    process.Start();
    process.WaitForExit();

    if (process.ExitCode != 0)
    {
        var error = process.StandardError.ReadToEnd();
        throw new Exception($"Flyway migration failed: {error}");
    }
}
