using Microsoft.IdentityModel.Tokens;
using System.Text;
using VirtualCatalogAPI.Helpers;
using VirtualCatalogAPI.Businesses.Email;
using VirtualCatalogAPI.Models.Email;
using System.Diagnostics;
using VirtualCatalogAPI.Businesses.Auth;
using VirtualCatalogAPI.Businesses.Categories;
using VirtualCatalogAPI.Businesses.Products;
using VirtualCatalogAPI.Businesses.Users;
using VirtualCatalogAPI.Data.Repository.Auth;
using VirtualCatalogAPI.Data.Repository.Categories;
using VirtualCatalogAPI.Data.Repository.Products;
using VirtualCatalogAPI.Data.Repository.Users;
using VirtualCatalogAPI.Data.Connection;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load environment variables
builder.Configuration.AddEnvironmentVariables();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

Env.Load();

builder.Services.Configure<EmailSettings>(options =>
{
    options.SmtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
    options.SmtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
    options.SmtpUser = Environment.GetEnvironmentVariable("SMTP_USER");
    options.SmtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
    options.FromEmail = Environment.GetEnvironmentVariable("FROM_EMAIL");
});

// Register services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<DatabaseHelper>();

builder.Services.AddHttpContextAccessor();

// Configure JWT
builder.Services.AddSingleton<JwtHelper>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new JwtHelper(
        configuration["Jwt:Key"]!,
        configuration["Jwt:Issuer"]!,
        configuration["Jwt:Audience"]!
    );
});

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
        policy.WithOrigins(
            "http://localhost:5173", // Allow localhost
            "https://www.devbychris.com/virtual-catalog/" // Allow domain
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Run Flyway migration
//RunFlywayMigration();

var app = builder.Build();

// Configure middleware
app.UseCors("AllowReactApp");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

static void RunFlywayMigration()
{
    try
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
            Console.WriteLine($"Flyway migration failed: {error}");
        }
        else
        {
            Console.WriteLine("Flyway migration completed successfully.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during Flyway migration: {ex.Message}");
    }
}
