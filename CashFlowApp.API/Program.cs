using AutoMapper;
using CashFlowApp.API.Configs;
using CashFlowApp.API.Middleware;
using CashFlowApp.BusinessLogic.Services;
using CashFlowApp.Models.Mappings;
using CashFlowApp.Repositories.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddDbContext<CashFlowContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CashFlow")));

var mapperConfig = new MapperConfiguration(config => { config.AddProfile(new ApplicationProfile()); });
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddExceptionHandler<ExceptionHandler>();
//builder.Services.AddTransient<ExceptionHandlerMiddleware>();

builder.Services.AddHttpClient<ITodoService, TodoService>();

builder.Services.AddConfigurationServices();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
//             ValidAudience = builder.Configuration["Jwt:ValidAudience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
//         };
//     });

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

app.UseExceptionHandler(opt => { });
//app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();