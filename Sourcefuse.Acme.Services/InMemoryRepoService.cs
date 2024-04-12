using Sourcefuse.Acme.Common.Interfaces;
using System.Collections.Concurrent;

namespace Sourcefuse.Acme.Services
{
    public class InMemoryRepoService : IAsyncRepoService
    {
        private static readonly ConcurrentDictionary<Guid, ICustomer> customers = [];
        private static readonly ConcurrentDictionary<Guid, IOrder> orders = [];

        private static bool CustomerExists(Guid customerId) 
            => customers.ContainsKey(customerId);

        private static bool OrderExists(Guid orderId)
            => orders.ContainsKey(orderId);

        public async Task<ICustomer> AddCustomer(ICustomer customer)
            => await Task.FromResult(customers.AddOrUpdate(customer.Id, customer, (key, oldvalue) => customer));

        public async Task<ICustomer> UpdateCustomer(Guid customerId, ICustomer customer)
            => CustomerExists(customerId) ?
                await Task.FromResult(customers.AddOrUpdate(customerId, customer, (key, oldvalue) => customer)) :
                throw new Exception("Customer not found");

        public async Task<ICustomer?> GetCustomer(Guid customerId)
            => await Task.FromResult(customers.TryGetValue(customerId, out var customer) ? customer : null);

        public async Task<IEnumerable<ICustomer>> GetCustomers()
            => await Task.FromResult(customers.Values);

        public async Task<IOrder> AddOrder(IOrder order)
           => CustomerExists(order.CustomerId) ?
                await Task.FromResult(orders.AddOrUpdate(order.Id, order, (key, oldvalue) => order)) :
                throw new Exception("Customer not found");

        public async Task<IOrder?> GetOrder(Guid orderId)
            => await Task.FromResult(orders.TryGetValue(orderId, out var order) ? order : null);

        public async Task<IEnumerable<IOrder>> GetOrders()
            => await Task.FromResult(orders.Values);

        public async Task<IEnumerable<IOrder>> GetOrdersForCustomer(Guid customerId)
            => CustomerExists(customerId) ?
                 await Task.FromResult(orders.Values.Where(o => o.CustomerId.Equals(customerId))) :
                 throw new Exception("Customer not found");

        public async Task<IOrder> UpdateOrder(Guid orderId, IOrder order)
           => OrderExists(orderId) ?
                await Task.FromResult(orders.AddOrUpdate(orderId, order, (key, oldvalue) => order)) :
                throw new Exception("Order not found");
    }
}
