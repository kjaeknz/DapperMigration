using System.Collections.Immutable;
using System.Data;
using System.Text;
using Dapper;
using DapperMigration.Models;
using DapperMigration.Persistence.Models;
using DapperMigration.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace DapperMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebServerController : ControllerBase
    {
        private readonly IWebServerRepository _webServerRepo;
        public WebServerController(IWebServerRepository webServerRepo)
        {
            _webServerRepo = webServerRepo;
        }

        [HttpPost]
        public WebServerModel Create(string name)
        {
            var id = _webServerRepo.Add(name);
            return new WebServerModel()
            {
                Id = id,
                Name = name
            };
        }

        [HttpGet]
        public IEnumerable<WebServerModel> Get()
        {
            var webServer = _webServerRepo.GetAll();
            return webServer.Select(s => new WebServerModel()
            {
                Id = s.Id,
                Name = s.Name,
                Arn = s.Arn
            });
        }

        [HttpPut]
        public void Update(WebServerModel webServerModel)
        {
            _webServerRepo.Update(webServerModel.Id, webServerModel.Name, webServerModel.Arn);
            return;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _webServerRepo.Delete(id);
            return;
        }
    }
}
