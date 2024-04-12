using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.Data.Configuration;
using Sourcefuse.Acme.Data.Extensions;
using Sourcefuse.Acme.Data.Models;

namespace Sourcefuse.Acme.Data.Contexts
{
    public partial class AcmeDbContext(IOptions<DbSettings> settings) : DbContext
    {
        public DbSettings Settings = settings.Value;

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration())
                        .ApplyConfiguration(new OrderEntityConfiguration());


            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges() => SaveChangesAsync().Result;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (!Settings.AllowDelete)
                ChangeTracker.ImplementNoDelete(Settings.ErrorOnDelete);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Implementation of IAsyncRepoService
    /// (Yes, it is lazy to use the dbcontext as the service)
    /// </summary>
    public partial class AcmeDbContext : IAsyncRepoService
    {
        public async Task<ICustomer> AddCustomer(ICustomer customer)
        {
            var result = Customers.Add((Customer)customer);

            var count = await SaveChangesAsync();

            return (count == 1) ? (ICustomer)result : throw new Exception("Error adding customer");
        }

        public async Task<IOrder> AddOrder(IOrder order)
        {
            var result = Orders.Add((Order)order);

            var count = await SaveChangesAsync();

            return (count == 1) ? (IOrder)result : throw new Exception("Error adding order");
        }

        public async Task<ICustomer?> GetCustomer(Guid customerId)
            => await Customers.FindAsync(customerId);

        public async Task<IEnumerable<ICustomer>> GetCustomers()
            => await Task.FromResult(Customers);

        public async Task<IOrder?> GetOrder(Guid orderId)
            => await Orders.FindAsync(orderId);

        public async Task<IEnumerable<IOrder>> GetOrders()
            => await Task.FromResult(Orders);

        public async Task<IEnumerable<IOrder>> GetOrdersForCustomer(Guid customerId)
            => await Task.FromResult(Orders.Where(o => o.CustomerId == customerId));

        public Task<ICustomer> UpdateCustomer(Guid customerId, ICustomer customer)
        {
            throw new NotImplementedException();
        }

        public Task<IOrder> UpdateOrder(Guid orderId, IOrder order)
        {
            throw new NotImplementedException();
        }
    }
}
