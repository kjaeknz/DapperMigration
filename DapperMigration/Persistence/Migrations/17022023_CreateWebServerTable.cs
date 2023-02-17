using System.Data;
using Dapper;

namespace DapperMigration.Persistence.Migrations
{
    public class _17022023_CreateWebServerTable : IMigration
    {
        public void Apply(IDbConnection connection)
        {
            connection.Execute(@"CREATE TABLE `webserver` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  `arn` VARCHAR(255) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC),
UNIQUE INDEX `name_UNIQUE` (`name` ASC));");
    
        }

        public void Rollback(IDbConnection connection)
        {
            connection.Execute("DROP TABLE `webserver`");
        }
    }
}
