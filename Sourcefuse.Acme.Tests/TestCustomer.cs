using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.Common.Models;
using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.Tests
{
    internal class TestCustomer : ICustomer
    {
        public TestCustomer()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public AddressModel Address { get; set; } = new AddressModel();

        [JsonExtensionData]
        public Dictionary<string, object> Properties { get; set; } = [];
    }
}
