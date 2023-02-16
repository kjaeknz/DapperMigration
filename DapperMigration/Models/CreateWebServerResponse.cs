namespace DapperMigration.Models
{
    /// <summary>
    /// Create a web server response
    /// </summary>
    public class CreateWebServerResponse
    {
        /// <summary>
        /// EC2 Instance ID
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// EC2 Instance Name
        /// </summary>
        public string? Name { get; set; }
    }
}
