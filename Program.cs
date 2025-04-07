using iPortal.Config;
using iPortal.Data;
using iPortal.Data.Repositories;
using iPortal.Mappings;
using iPortal.Security;
using iPortal.Exceptions;
using iPortal.Services.Implementations;
using iPortal.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure DbContext for MySQL
builder.Services.AddDbContext<IPortalDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Configure AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<MappingProfile>();
    config.AllowNullCollections = true;
}, typeof(MappingProfile));

// Dependency Injection
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserDetailsService, CustomUserDetailsService>();
builder.Services.AddScoped<JwtUtil>(sp => new JwtUtil(builder.Configuration));
builder.Services.AddScoped<JwtEntryPoint>();
builder.Services.AddScoped<ISecurityService, SecurityServiceImpl>();
builder.Services.AddScoped<IStudentService, StudentServiceImpl>();
builder.Services.AddScoped<IEmployerService, EmployerServiceImpl>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<EmployerRepository>();
builder.Services.AddScoped<TokenRepository>();
builder.Services.AddScoped<RoleRepository>();

// JWT Authentication
var secretKey = builder.Configuration["Jwt:SecretKey"];
if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("Jwt:SecretKey is not configured in appsettings.json.");
}
var key = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://ip-fe.vercel.app")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Configure server port
var port = builder.Configuration.GetValue<int>("Server:Port");
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseMiddleware<JwtFilter>();
app.UseAuthorization();

app.MapControllers();

app.Run();