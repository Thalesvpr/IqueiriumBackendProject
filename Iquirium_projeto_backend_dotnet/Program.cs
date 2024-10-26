using Iquirium_projeto_backend_dotnet.Src.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar a string de conex�o usando appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Adicionar o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Adicionar controladores e outras depend�ncias
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
