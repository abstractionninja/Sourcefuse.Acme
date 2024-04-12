using Sourcefuse.Acme.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.WebApi.Models
{
    /// <summary>
    /// Implementation of <see cref="IOrder"/>
    /// </summary>
    public class OrderModel : IOrder
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public OrderModel()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Unique Id
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Unique Id of Parent Customer Object
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Dictionary of additional key/value pairs 
        /// <seealso cref="JsonExtensionDataAttribute"/>
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, object> OrderDetails { get; set; } = [];
    }
}
