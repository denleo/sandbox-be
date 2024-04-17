namespace Sandbox.Contracts.Events.User;

public class UserCreatedEvent
{
    public required string FirebaseId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public bool IsEmailVerified { get; set; }
    public string? AvatarUrl { get; set; }
}