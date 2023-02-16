using System.Data;
using Dapper;
using MySqlConnector;

namespace DapperMigration.Persistence
{
    public class MigrationService
    {
        private readonly string _connectionString;
        private string _dbName = string.Empty;

        public MigrationService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SeedDatabase()
        {
            var serverConnectionString = GetServerConnectionString();

            using IDbConnection connection = new MySqlConnection( serverConnectionString );
            if (DatabaseExists(connection)) return;

            connection.Execute("CREATE DATABASE " + _dbName, new
            {
                dbname = _dbName
            });
            connection.Open();
            connection.ChangeDatabase(_dbName);
            connection.Execute(@"CREATE TABLE `webserver` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  `arn` VARCHAR(255) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
UNIQUE INDEX `name_UNIQUE` (`name` ASC));");
        }

        private bool DatabaseExists(IDbConnection connection)
        {
            var result = connection.ExecuteScalar<string>("SHOW DATABASES LIKE @dbname", new
                { dbname = _dbName });
            return !string.IsNullOrEmpty(result);
        }

        private string GetServerConnectionString()
        {
            var serverConnectionString = string.Empty;
            var connectionStringOptions = _connectionString.Split(';');
            var dbOptionPrefix = "database=";
            for (int i = 0; i < connectionStringOptions.Length; i++)
            {
                if (connectionStringOptions[i].IndexOf(dbOptionPrefix, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    serverConnectionString += connectionStringOptions[i] + ";";
                }
                else
                {
                    _dbName = connectionStringOptions[i].Substring(dbOptionPrefix.Length);
                }
            }
            return serverConnectionString;
        }
    }
}
