using System.Security.Claims;
using Sandbox.Contracts.Auth;

namespace Sandbox.Wordbook.API.Contexts;

public class AuthenticatedContext(IHttpContextAccessor contextAccessor) : IAuthenticatedContext
{
    private readonly ClaimsPrincipal _user = contextAccessor.HttpContext!.User;

    public string FirebaseId => _user.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

    public string Name => _user.Claims.First(claim => claim.Type == "name").Value;

    public string Email => _user.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

    public bool IsEmailVerified => bool.Parse(_user.Claims.First(claim => claim.Type == "email_verified").Value);
}