using CarSelling.Data;
using CarSelling.Infrastructure;
using CarSelling.Repositories;
using CarSelling.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = JwtValidation.TokenValidationParameters;
    });

builder.Services.AddAuthorization();

// Add services to the container.
var mvcBuilder = builder.Services.AddControllers(
    config => config.Filters.Add<FormatFilter>());

mvcBuilder.AddJsonOptions(config =>
{
    config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

mvcBuilder.AddXmlSerializerFormatters();

builder.Services.AddDbContext<CarSellingDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CarSellingDatabase")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserPrincipal>();
builder.Services.AddScoped<IAuthorizationHandler, CarAdPermissionHandler>();
builder.Services.AddScoped<IAuthorizationHandler, UserPermissionHandler>();

builder.Services.AddScoped<ICarAdRepository, CarAdRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFilePathRepository, FilePathRepository>();

builder.Services.AddScoped<ICarAdService, CarAdService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler("/api/Error");

app.UseMiddleware<JwtSettingsMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();