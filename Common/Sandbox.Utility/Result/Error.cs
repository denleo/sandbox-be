namespace Sandbox.Utility.Result;

/// <summary>
///     Represents known domain error in the application
/// </summary>
public record Error(string Name, string? Description)
{
    public static Error None => new(string.Empty, string.Empty);
}