namespace Gateway.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string FirebaseId { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public bool IsEmailVerified { get; set; }
    public string? AvatarUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
}