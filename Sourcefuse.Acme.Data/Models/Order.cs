using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.Data.Abstracts;
using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.Data.Models
{
    public class Order :EntityBase, IOrder
    { 
        public Guid CustomerId { get; set; }

        [JsonExtensionData]
        public Dictionary<string,object> OrderDetails { get; set; } = [];

        public virtual required Customer Customer { get; set; }
    }

}
