using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sourcefuse.Acme.Data.Abstracts;
using Sourcefuse.Acme.Data.Models;

namespace Sourcefuse.Acme.Data.Configuration
{
    public class CustomerEntityConfiguration : EntityConfigurationBase<Customer>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Customer> builder)
        {
            builder.OwnsOne(e => e.Address, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

            builder.OwnsOne(e => e.Properties, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

            builder.HasMany(e => e.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId);
        }
    }
}
