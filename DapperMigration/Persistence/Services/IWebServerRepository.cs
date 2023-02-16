using System.Data;
using DapperMigration.Persistence.Models;

namespace DapperMigration.Persistence.Services
{
    public interface IWebServerRepository
    {
        int Add(string name);
        void Update(int id, string? name, string? arn);
        IEnumerable<WebServer> GetAll();
        void Delete(int id);
    }
}
