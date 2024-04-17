using Gateway.Data;
using Gateway.Data.Entities;
using Gateway.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Services;

public class UserService(IdentityContext context) : IUserService
{
    public async Task CreateUserAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public Task<bool> IsUserExists(string firebaseId)
    {
        return context.Users.AnyAsync(x => x.FirebaseId == firebaseId);
    }
}