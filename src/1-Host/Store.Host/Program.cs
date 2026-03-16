using System.IdentityModel.Tokens.Jwt;
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

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

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
builder.Services.AddScoped<ILoginAppService, LoginAppService>();

// Service
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IVendaService, VendaService>();
builder.Services.AddScoped<ILoginService, LoginService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "Administrador"));
    options.AddPolicy("ClienteOnly", policy => policy.RequireClaim("role", "Cliente"));
    options.AddPolicy("Everyone", policy => policy.RequireClaim("role", "Administrador", "Cliente"));
});

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
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
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