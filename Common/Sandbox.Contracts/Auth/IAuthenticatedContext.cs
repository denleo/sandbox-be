namespace Sandbox.Contracts.Auth;

public interface IAuthenticatedContext
{
    public string FirebaseId { get; }
    public string Name { get; }
    public string Email { get; }
    public bool IsEmailVerified { get; }
}