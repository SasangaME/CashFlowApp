using AutoMapper;
using CashFlowApp.API.Configs;
using CashFlowApp.API.Middleware;
using CashFlowApp.Models.Mappings;
using CashFlowApp.Repositories.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CashFlowContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CashFlow")));

var mapperConfig = new MapperConfiguration(config => { config.AddProfile(new ApplicationProfile()); });
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<ExceptionHandlerMiddleware>();

builder.Services.AddConfigurationServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();