using Gateway.Data.Entities;

namespace Gateway.Services.Abstractions;

public interface IUserService
{
    Task CreateUserAsync(User user);
    Task<bool> IsUserExists(string firebaseId);
}