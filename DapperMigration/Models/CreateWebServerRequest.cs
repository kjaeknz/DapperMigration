using System.ComponentModel.DataAnnotations;

namespace DapperMigration.Models
{
    /// <summary>
    /// Request parameters for creating web server hosting resource
    /// </summary>
    public class CreateWebServerRequest
    {
        /// <summary>
        /// Web Server Name
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// Image Id
        /// </summary>
        [Required]
        public string? AmiId { get; set; }

        /// <summary>
        /// Instance Type
        /// </summary>
        [Required]
        public string? InstanceType { get; set; }

        /// <summary>
        /// Key pair key name used for encryption
        /// </summary>
        [Required]
        public string? AdminPasswordKeyPair { get; set; }

        /// <summary>
        /// Subnet Id to use for this instance
        /// </summary>
        [Required]
        public string? SubnetId { get; set; }

        /// <summary>
        /// Security group IDs for this instance
        /// </summary>
        [MinLength(1, ErrorMessage = "The {0} field should not be empty.")]
        public IEnumerable<string>? SecurityGroupIds { get; set; }

        /// <summary>
        /// Iam ARN instance profile that this instance is attached
        /// </summary>
        [Required]
        public string? IamInstanceProfileArn { get; set; }
    }
}
