using Microsoft.EntityFrameworkCore;
using Sandbox.Wordbook.Domain;
using Sandbox.Wordbook.Domain.Abstractions;

namespace Sandbox.Wordbook.Persistence;

public class WordbookContext: DbContext, IUnitOfWork
{
    public DbSet<WordbookUser> Users { get; set; }
    public DbSet<Translation> Translations { get; set; }
    public DbSet<TranslationResult> TranslationResults { get; set; }
    
    public WordbookContext(DbContextOptions<WordbookContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WordbookContext).Assembly);
    }
}