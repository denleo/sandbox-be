using System.Security.Claims;
using Gateway.Data.Entities;
using Gateway.Services.Abstractions;
using MassTransit;
using Sandbox.Contracts.Events.User;

namespace Gateway.Middlewares;

public class IdentityMiddleware
{
    private readonly RequestDelegate _next;

    public IdentityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IUserService userService,
        IPublishEndpoint publishEndpoint,
        ILogger<IdentityMiddleware> logger)
    {
        if (context.User.Identity!.IsAuthenticated)
        {
            var id = context.User.Claims.First(x => x.Type == "user_id").Value;
            var isUserExists = await userService.IsUserExists(id);
            if (!isUserExists)
            {
                var user = new User
                {
                    FirebaseId = id,
                    FullName = context.User.Claims.First(x => x.Type == "name").Value,
                    Email = context.User.Claims.First(x => x.Type == ClaimTypes.Email).Value,
                    IsEmailVerified = bool.Parse(context.User.Claims.First(x => x.Type == "email_verified").Value),
                    AvatarUrl = context.User.Claims.First(x => x.Type == "picture").Value,
                    CreatedAt = DateTime.UtcNow
                };

                await userService.CreateUserAsync(user);

                await publishEndpoint.Publish(new UserCreatedEvent
                {
                    FirebaseId = user.FirebaseId,
                    FullName = user.FullName,
                    Email = user.Email,
                    IsEmailVerified = user.IsEmailVerified,
                    AvatarUrl = user.AvatarUrl
                });

                logger.LogInformation("[Identity] New user was created: {fullname} ({id})",
                    user.FullName,
                    user.Id);
            }
        }

        await _next(context);
    }
}

public static class IdentityMiddlewareExtensions
{
    public static IApplicationBuilder UseIdentityMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<IdentityMiddleware>();
    }
}