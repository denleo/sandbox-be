using Gateway.Data;
using Gateway.MessageBroker;
using Gateway.Middlewares;
using Gateway.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<IdentityContext>(
    opt =>
    {
        opt.UseNpgsql(config.GetConnectionString("IdentityDatabase"));
        opt.EnableSensitiveDataLogging();
        opt.EnableDetailedErrors();
    });

builder.Services.RegisterServices();

builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.Authority = config["Authentication:Issuer"];
        opt.Audience = config["Authentication:Audience"];
        opt.TokenValidationParameters.ValidIssuer = config["Authentication:Issuer"];

        opt.TokenValidationParameters.ValidateLifetime = true;
        opt.TokenValidationParameters.ValidateAudience = true;
        opt.TokenValidationParameters.ValidateIssuer = true;
    });

builder.Services.AddBus(config);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("authenticated", policy => policy.RequireAuthenticatedUser());

builder.Services.AddReverseProxy()
    .LoadFromConfig(config.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityMiddleware();

app.MapGet("/health", () => Results.Ok()).RequireAuthorization("authenticated");
app.MapReverseProxy();

app.EnsureDatabaseMigration();

app.Run();