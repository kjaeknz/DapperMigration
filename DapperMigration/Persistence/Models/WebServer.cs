namespace DapperMigration.Persistence.Models
{
    public class WebServer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Arn { get; set; }
    }
}
