using Microsoft.EntityFrameworkCore;

namespace Gateway.Data;

public static class DatabaseMigrator
{
    public static void EnsureDatabaseMigration(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateAsyncScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IdentityContext>>();

            try
            {
                context.Database.Migrate();
                logger.LogInformation("Database has been successfully migrated...");
            }
            catch (Exception e)
            {
                logger.LogCritical(e, "Exception occured while database migration:\r\n{message}", e.Message);
                throw;
            }
        }
    }
}