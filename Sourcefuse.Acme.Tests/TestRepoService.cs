using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.Data.Models;

namespace Sourcefuse.Acme.Tests
{
    internal class TestRepoService : IAsyncRepoService
    {

        private readonly List<ICustomer> _customers  = [];
        private readonly List<IOrder> _orders = [];

        public async Task<ICustomer> AddCustomer(ICustomer customer)
        {
           _customers.Add(customer);
           return await Task.FromResult(customer);
        }

        public async Task<IOrder> AddOrder(IOrder order)
        {
            _orders.Add(order);
            return await Task.FromResult(order);
        }

        public async Task<ICustomer?> GetCustomer(Guid customerId)
            => await Task.FromResult(_customers.Find(c => c.Id.Equals(customerId)));

        public async Task<IEnumerable<ICustomer>> GetCustomers()
            => await Task.FromResult(_customers);

        public async Task<IOrder?> GetOrder(Guid orderId)
            => await Task.FromResult(_orders.Find(o => o.Id.Equals(orderId)));

        public async Task<IEnumerable<IOrder>> GetOrders()
            => await Task.FromResult(_orders);

        public async Task<IEnumerable<IOrder>> GetOrdersForCustomer(Guid customerId)
            => await Task.FromResult(_orders.Where(o => o.Id.Equals(customerId)));

        public async Task<ICustomer> UpdateCustomer(Guid customerId, ICustomer customer)
        {
            var target = _customers.FirstOrDefault(c => c.Id.Equals(customerId))??new TestCustomer();
            _customers.Remove(target);
            _customers.Add(customer);

            return await Task.FromResult(customer);
        }

        public async Task<IOrder> UpdateOrder(Guid orderId, IOrder order)
        {
            var target = _orders.FirstOrDefault(c => c.Id.Equals(orderId)) ?? new TestOrder();
            _orders.Remove(target);
            _orders.Add(order);

            return await Task.FromResult(order);

        }
    }
}
