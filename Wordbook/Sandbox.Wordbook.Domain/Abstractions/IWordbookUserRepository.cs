namespace Sandbox.Wordbook.Domain.Abstractions;

public interface IWordbookUserRepository
{
    void CreateUser(WordbookUser wordbookUser);
    Task<WordbookUser?> GetUserByIdAsync(Guid id, CancellationToken token = default);
    Task<WordbookUser?> GetUserByFirebaseIdAsync(string firebaseId, CancellationToken token = default);
    Task<bool> IsUserExists(string firebaseId, CancellationToken token = default);
}