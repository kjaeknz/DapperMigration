using System.Data;

namespace DapperMigration.Persistence.Migrations
{
    public interface IMigration
    {
        void Apply(IDbConnection connection);
        void Rollback(IDbConnection connection);
    }
}
