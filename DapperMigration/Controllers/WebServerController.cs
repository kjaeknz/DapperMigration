using Dapper;
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
        public string Create()
        {
            return string.Empty;
        }
    }
}
