using Sourcefuse.Acme.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.Tests
{
    internal class TestOrder : IOrder
    {
        public TestOrder() { 
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid CustomerId { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> OrderDetails { get; set; } = [];
    }
}
