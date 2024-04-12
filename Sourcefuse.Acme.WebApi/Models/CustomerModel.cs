using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.WebApi.Models
{
    /// <summary>
    /// Implementation of <see cref="ICustomer"/>
    /// </summary>
    public class CustomerModel : ICustomer
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CustomerModel()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Unique Identifier
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// First name of customer
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Last name of customer
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Email address of customer
        /// </summary>
        [EmailAddress]
        public required string Email { get; set; }

        /// <summary>
        /// Address of customer
        /// <seealso cref="AddressModel"/>
        /// </summary>
        public AddressModel Address { get; set; } = new AddressModel();

        /// <summary>
        /// Dictionary of additional key/value pairs
        /// <seealso cref="JsonExtensionDataAttribute"/>
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> Properties { get; set; } = [];
    }
}
