using IqueiriumBackendProject.Src.Application.Services.Auth;
using IqueiriumBackendProject.Src.Application.Services.Member;
using IqueiriumBackendProject.Src.Application.Services.Products;
using IqueiriumBackendProject.Src.Application.Services.Users;
using IqueiriumBackendProject.Src.Infrastructure.Auth;
using IqueiriumBackendProject.Src.Infrastructure.Data;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext ao contêiner de serviços
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));




// Configura o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductFeedbackService>();
builder.Services.AddScoped<ProductFeedbackAnalysisService>();
builder.Services.AddScoped<MemberFeedbackService>();




builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtConfig = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy =>
        policy.RequireRole("Admin")); // Adiciona uma policy para a role "Admin"
    options.AddPolicy("RequireAnalyst", policy =>
        policy.RequireRole("Analyst"));
});


builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication(); // Adiciona o middleware de autenticação
app.UseAuthorization();  // Adiciona o middleware de autorização


// Configura o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
}

app.UseHttpsRedirection();

app.Run();
