using Dapper;
using MySqlConnector;

namespace DapperMigration.Persistence
{
    public class MigrationService
    {
        private readonly string _dbName; 
        private readonly MySqlConnection _connection;

        public MigrationService(MySqlConnection connection)
        {
            _connection = connection;
            _dbName = "axis_database";
        }

        public void SeedDatabase()
        {
            _connection.Open();
            if (DatabaseExists()) return;

            _connection.Execute("CREATE DATABASE " + _dbName, new
            {
                dbname = _dbName
            });
            _connection.ChangeDatabase(_dbName);
            _connection.Execute(@"CREATE TABLE `webserver` (
  `id` INT NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `arn` VARCHAR(255) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC));");
        }

        private bool DatabaseExists()
        {
            var result = _connection.ExecuteScalar<string>("SHOW DATABASES LIKE @dbname", new
                { dbname = _dbName });
            return !string.IsNullOrEmpty(result);
        }
    }
}
