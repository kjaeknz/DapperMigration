using DapperMigration.Persistence;

namespace DapperMigration.BackgroundService
{
    public class SeedDatabase : IHostedService
    {
        private readonly MigrationService _migrationService;

        public SeedDatabase(MigrationService migrationService)
        {
            _migrationService = migrationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Do your startup work here
            _migrationService.SeedDatabase();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // We have to implement this method too, because it is in the interface

            return Task.CompletedTask;
        }
    }
}
