using LiteDB;
using Store.Domain.Interfaces;
using Store.Infra.Data.NoSql;
using Store.AppService.Interfaces;
using Store.AppService;
using Store.Host;
using Store.Domain;

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

builder.Services.AddScoped<IClienteRepository, ClienteRepositoryNoSql>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepositoryNoSql>();
builder.Services.AddScoped<IVendaRepository, VendaRepositoryNoSql>();

builder.Services.AddScoped<IClienteAppService, ClienteAppService>();
builder.Services.AddScoped<IProdutoAppService, ProdutoAppService>();
builder.Services.AddScoped<IVendaAppService, VendaAppService>();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<VendaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();