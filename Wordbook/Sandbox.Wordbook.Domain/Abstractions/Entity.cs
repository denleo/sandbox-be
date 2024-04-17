namespace Sandbox.Wordbook.Domain.Abstractions;

public abstract class Entity<T>
{
    public T? Id { get; set; }
}