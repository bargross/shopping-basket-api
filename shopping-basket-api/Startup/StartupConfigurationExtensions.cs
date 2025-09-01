using shopping_basket_api.SQL;
using Microsoft.EntityFrameworkCore;

namespace shopping_basket_api.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            var skipMigrations = app.Configuration.GetValue<bool>("Database:SkipMigration");
            if (skipMigrations)
            {
                return;
            }

            using var scope = app.Services.CreateAsyncScope();
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                LogPendingMigrations(context);

                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private static void LogPendingMigrations(AppDbContext context)
        {
            var pending = context.Database.GetPendingMigrations();
            if (pending.Any())
            {
                Console.WriteLine("Pending migrations: " + string.Join(", ", pending));
            }
        }
    }
}
