using DotNetEnv;
using EmployeesManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Build database connection string from environment variables
var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
var dbDatabase = Environment.GetEnvironmentVariable("DB_DATABASE");
var dbTrustedConnection = Environment.GetEnvironmentVariable("DB_TRUSTED_CONNECTION");
var dbMultipleActiveResultSets = Environment.GetEnvironmentVariable("DB_MULTIPLE_ACTIVE_RESULT_SETS");
var dbTrustServerCertificate = Environment.GetEnvironmentVariable("DB_TRUST_SERVER_CERTIFICATE");

// Windows Authentication
string connectionString =   $"Server={dbServer};" +
                            $"Database={dbDatabase};" +
                            $"Trusted_Connection={dbTrustedConnection};" +
                            $"MultipleActiveResultSets={dbMultipleActiveResultSets};"+
                            $"TrustServerCertificate={dbTrustServerCertificate};";

// Add MSSql Server dbContext
builder.Services.AddDbContext<EmpMgtSysDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add Angular CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins(
            "http://localhost:4200",
            "https://localhost:4200" // Update according to Angular app URL
            ).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});

// Add React CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000" // Update according to React app URL
            ).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
