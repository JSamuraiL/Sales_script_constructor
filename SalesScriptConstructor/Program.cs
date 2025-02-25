using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesScriptConstructor.Domain.Interfaces.ISellers;
using SalesScriptConstructor.Infrastructure;
using SalesScriptConstructor.Domain.Services;
using SalesScriptConstructor.Domain.Interfaces.IManagers;
using SalesScriptConstructor.Infrastructure.Repositories;
using SalesScriptConstructor.Domain.Interfaces.IScripts;
using SalesScriptConstructor.Domain.Interfaces.IBlocks;
using SalesScriptConstructor.Domain.Interfaces.IBlockConnections;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration().MinimumLevel.Warning().WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IManagersService, ManagersService>();
builder.Services.AddTransient<IManagersRepository, ManagersRepository>();
builder.Services.AddTransient<ISellersService, SellersService>();
builder.Services.AddTransient<ISellersRepository, SellersRepository>();
builder.Services.AddTransient<IScriptsService, ScriptsService>();
builder.Services.AddTransient<IScriptsRepository, ScriptsRepository>();
builder.Services.AddTransient<IBlocksService, BlocksService>();
builder.Services.AddTransient<IBlocksRepository, BlocksRepository>();
builder.Services.AddTransient<IBlockConnectionsService, BlockConnectionsService>();
builder.Services.AddTransient<IBlockConnectionsRepository, BlockConnectionsRepository>();
builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� ��� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
