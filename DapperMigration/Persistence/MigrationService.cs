using System.Data;
using Dapper;
using DapperMigration.Persistence.Migrations;
using DapperMigration.Persistence.Migrations.Models;
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

        public void CreateDatabase()
        {
            var serverConnectionString = GetServerConnectionString();

            using IDbConnection connection = new MySqlConnection(serverConnectionString);
            if (DatabaseExists(connection)) return;

            connection.Execute("CREATE DATABASE " + _dbName, new
            {
                dbname = _dbName
            });

            CreateMigrationTable();
        }

        public void ApplyMigrations()
        {
            using IDbConnection connection = new MySqlConnection( _connectionString );
            var migrationsToApply = new List<string>();
            var fullName = typeof(_17022023_CreateWebServerTable).FullName;
            if (fullName != null)
                migrationsToApply.Add(fullName);

            var migrationsApplied = connection.Query<Migration>("SELECT * FROM `migration`");
            for (int i = 0; i < migrationsToApply.Count; i++)
            {
                if (migrationsApplied.Count() >= i+1)
                {
                    if (!migrationsApplied.Any(m => m.Id == i+1 && m.Name.Equals(migrationsToApply[i])))
                    {
                        throw new Exception("Cannot perform migration. Missing migration " + migrationsToApply[i]);
                    }
                }
                else
                {
                    var type = Type.GetType(migrationsToApply[i]);
                    var migration = (IMigration)Activator.CreateInstance(type);
                    migration.Apply(connection);
                    connection.Execute("INSERT INTO `migration`(name) VALUES(@name)",
                        new { name = migrationsToApply[i] });
                }
            }
        }

        public void CreateMigrationTable()
        {
            using IDbConnection connection = new MySqlConnection(_connectionString);
            connection.Execute(@"CREATE TABLE `migration` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
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
