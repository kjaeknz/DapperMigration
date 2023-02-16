namespace DapperMigration.Models
{
    /// <summary>
    /// Create a web server response
    /// </summary>
    public class WebServerModel
    {
        /// <summary>
        /// EC2 Instance ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// EC2 Instance Name
        /// </summary>
        public string Name { get; set; }
        public string? Arn { get; set; }
    }
}
