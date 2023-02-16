using Dapper;
using DapperMigration.Persistence.Models;
using MySqlConnector;
using System.Text;
using System;
using System.Data;

namespace DapperMigration.Persistence.Services
{
    public class WebServerRepository : IWebServerRepository
    {
        private readonly IDbConnection _connection;
        public WebServerRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public int Add(string name)
        {
            var webServerToInsert = new WebServer()
            {
                Name = name,
            };
            var query = "INSERT INTO webserver(name, arn) VALUES(@Name, @Arn);" +
                        "select LAST_INSERT_ID()";
            _connection.Open();
            var id = _connection.ExecuteScalar<int>(query, webServerToInsert);
            return id;
        }

        public void Update(int id, string? name, string? arn)
        {
            var sb = new StringBuilder("UPDATE webserver SET ");
            if (name != null) sb.Append("name = @name,");
            if (arn != null) sb.Append("arn = @arn,");
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" WHERE id = @id;");
            var query = sb.ToString();

            _connection.Execute(query, new {name = name, arn = arn, id = id});
        }

        public IEnumerable<WebServer> GetAll()
        {
            var query = ("SELECT * FROM webserver");
            var webServer = _connection.Query<WebServer>(query);
            return webServer;
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM webserver where id = @id";
            _connection.Execute(query, new { id = id });
            return;
        }
    }
}
