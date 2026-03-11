using System.Text;
using LiteDB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Store.AppService;
using Store.AppService.Interfaces;
using Store.Domain;
using Store.Domain.Interfaces;
using Store.Host;
using Store.Infra.Data.NoSql;
using Store.Infra.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var databasePath = builder.Configuration["LiteDb:DatabasePath"] ?? "Data/Store.db";

var directory = Path.GetDirectoryName(databasePath);
if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
{
    Directory.CreateDirectory(directory);
}

// Configurar o BsonMapper
var mapper = LiteDbConfig.ConfigureMapper();

var connectionString = $"Filename={databasePath};Connection=shared";
builder.Services.AddSingleton<ILiteDatabase>(new LiteDatabase(connectionString, mapper));

// Repository
builder.Services.AddScoped<IClienteRepository, ClienteRepositoryNoSql>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepositoryNoSql>();
builder.Services.AddScoped<IVendaRepository, VendaRepositoryNoSql>();

// AppService
builder.Services.AddScoped<IClienteAppService, ClienteAppService>();
builder.Services.AddScoped<IProdutoAppService, ProdutoAppService>();
builder.Services.AddScoped<IVendaAppService, VendaAppService>();

// Service
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IVendaService, VendaService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true, // Verifica se o token expirou
            ClockSkew = TimeSpan.Zero // Remove tempo extra de tolerância
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();