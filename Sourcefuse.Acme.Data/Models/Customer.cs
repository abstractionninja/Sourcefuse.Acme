using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.Common.Models;
using Sourcefuse.Acme.Data.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.Data.Models
{
    public class Customer: EntityBase, ICustomer
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public AddressModel Address { get; set; } = new AddressModel();

        [JsonExtensionData]
        public Dictionary<string, object> Properties { get; set; } = [];

        public virtual IEnumerable<Order> Orders { get; set; } = [];

    }

}
