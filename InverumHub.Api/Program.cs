using InverumHub.Api.Common.JWT;
using InverumHub.Core.Common;
using InverumHub.Core.Mappers;
using InverumHub.Core.Repositories;
using InverumHub.Core.Services;
using InverumHub.DataLayer.Extensions;
using InverumHub.DataLayer.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);


Env.Load();

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInverumHubDataLayer(builder.Configuration.GetConnectionString("Default")!);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//JWT Configuration
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWTConfig"));
builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();
AuthConfig.ConfigureJwt(builder.Services, builder.Configuration);


// repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IPermissionsRepository, PermissionsRepository>();



//services
builder.Services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPermissionsService, PermissionsService>();

builder.Services.AddAutoMapper(typeof(UserMapperProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            BusinessException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        await context.Response.WriteAsJsonAsync(new
        {
            error = exception?.Message
        });

    });
});



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
