using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.Common.Interfaces
{
    public interface IOrder
    {
        public Guid Id { get; }
        public Guid CustomerId { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> OrderDetails { get; set; }
    }
}
