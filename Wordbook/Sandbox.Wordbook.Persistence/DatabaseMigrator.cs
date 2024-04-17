using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Sandbox.Wordbook.Persistence;

public class DatabaseMigrator
{
    private readonly WordbookContext _context;
    private readonly ILogger<DatabaseMigrator> _logger;

    public DatabaseMigrator(WordbookContext context, ILogger<DatabaseMigrator> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public void Migrate()
    {
        try
        {
            _context.Database.Migrate();
            _logger.LogInformation("[Wordbook] Database has been successfully migrated...");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "[Wordbook] Exception occured while database migration:\r\n{message}", e.Message);
            throw;
        }
    }
}