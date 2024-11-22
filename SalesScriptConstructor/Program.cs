using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesScriptConstructor.Domain.Interfaces.ISellers;
using SalesScriptConstructor.Infrastructure;
using SalesScriptConstructor.Domain.Services;
using SalesScriptConstructor.Domain.Interfaces.IManagers;
using SalesScriptConstructor.Infrastructure.Repositories;
using SalesScriptConstructor.Domain.Interfaces.IScripts;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
