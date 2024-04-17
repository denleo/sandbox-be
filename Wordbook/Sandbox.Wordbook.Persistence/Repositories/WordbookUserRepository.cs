using Microsoft.EntityFrameworkCore;
using Sandbox.Wordbook.Domain;
using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Persistence.Repositories;

public class WordbookUserRepository : IWordbookUserRepository
{
    private readonly WordbookContext _context;

    public WordbookUserRepository(WordbookContext context)
    {
        _context = context;
    }

    public void CreateUser(WordbookUser wordbookUser)
    {
        _context.Users.Add(wordbookUser);
    }

    public Task<WordbookUser?> GetUserByIdAsync(Guid id, CancellationToken token = default)
    {
        return _context.Users
            .FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public Task<WordbookUser?> GetUserByFirebaseIdAsync(string firebaseId, CancellationToken token = default)
    {
        return _context.Users
            .FirstOrDefaultAsync(x => x.FirebaseId == firebaseId, token);
    }

    public Task<bool> IsUserExists(string firebaseId, CancellationToken token = default)
    {
        return _context.Users.AnyAsync(x => x.FirebaseId == firebaseId, token);
    }
}