using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sourcefuse.Acme.Data.Abstracts;
using Sourcefuse.Acme.Data.Models;

namespace Sourcefuse.Acme.Data.Configuration
{
    public class OrderEntityConfiguration : EntityConfigurationBase<Order>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(e => e.OrderDetails, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

            builder.HasOne(e => e.Customer)
                   .WithMany(o => o.Orders)
                   .HasForeignKey(o => o.CustomerId);
            
        }
    }
}
