using System.Collections.Immutable;
using System.Text;
using Dapper;
using DapperMigration.Models;
using DapperMigration.Persistence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace DapperMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebServerController : ControllerBase
    {
        private readonly MySqlConnection _connection;
        public WebServerController(MySqlConnection connection)
        {
            _connection = connection;
        }

        [HttpPost]
        public WebServerModel Create(string name)
        {
            var webServerToInsert = new WebServer()
            {
                Name = name,
            };
            var query = "INSERT INTO webserver(name, arn) VALUES(@Name, @Arn);" +
                        "select LAST_INSERT_ID()";
            _connection.Open();
            var id = _connection.ExecuteScalar<int>(query, webServerToInsert);
            return new WebServerModel()
            {
                Id = id,
                Name = name
            };
        }

        [HttpGet]
        public IEnumerable<WebServerModel> Get()
        {
            var query = ("SELECT * FROM webserver");
            _connection.Open();
            var webServer = _connection.Query<WebServer>(query);
            return webServer.Select(s => new WebServerModel()
            {
                Id = s.Id,
                Name = s.Name,
                Arn = s.Arn
            });
        }

        [HttpPut]
        public void Update(WebServer webServer)
        {
            var sb = new StringBuilder("UPDATE webserver SET ");
            if (webServer.Name != null) sb.Append("name = @name,");
            if (webServer.Arn != null) sb.Append("arn = @arn,");
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" WHERE id = @id;");
            var query = sb.ToString();
            _connection.Open();
            _connection.Execute(query, webServer);
            return;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var query = "DELETE FROM webserver where id = @id";
            _connection.Open();
            _connection.Execute(query, new { id = id });
            return;
        }
    }
}
