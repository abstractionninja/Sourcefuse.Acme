using Sourcefuse.Acme.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.Common.Interfaces
{
    public interface ICustomer
    {
        public Guid Id { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public AddressModel Address { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Properties { get; set; }

    }
}
