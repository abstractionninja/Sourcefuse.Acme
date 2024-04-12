namespace Sourcefuse.Acme.Common.Interfaces
{
    public interface IAsyncRepoService
    {
        public Task<IEnumerable<ICustomer>> GetCustomers();
        public Task<ICustomer?> GetCustomer(Guid customerId);
        public Task<ICustomer> AddCustomer(ICustomer customer);
        public Task<ICustomer> UpdateCustomer(Guid customerId, ICustomer customer);

        public Task<IEnumerable<IOrder>> GetOrders();
        public Task<IEnumerable<IOrder>> GetOrdersForCustomer(Guid customerId);
        public Task<IOrder?> GetOrder(Guid orderId);
        public Task<IOrder> AddOrder(IOrder order);
        public Task<IOrder> UpdateOrder(Guid orderId, IOrder order);


    }
}
