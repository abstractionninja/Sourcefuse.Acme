using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sourcefuse.Acme.Data.Abstracts
{
    public abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBase
    {
        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedDt)
                   .HasDefaultValueSql("getutcdate()");

            ConfigureEntity(builder);
        }
    }
}
