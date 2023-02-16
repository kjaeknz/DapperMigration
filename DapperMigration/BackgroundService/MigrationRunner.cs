using DapperMigration.Persistence;

namespace DapperMigration.BackgroundService
{
    public class MigrationRunner : IHostedService
    {
        private readonly MigrationService _migrationService;

        public MigrationRunner(MigrationService migrationService)
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
