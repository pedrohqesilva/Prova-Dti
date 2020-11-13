using Application.Domain.AggregatesModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Data.Context.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public virtual void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id)
               .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(p => p.Amount)
                .IsRequired();

            builder.Property(p => p.UnitPrice)
                .IsRequired();

            builder.Property(p => p.Valid)
                .IsRequired();

            builder
               .ToTable("Products")
               .HasKey(x => x.Id);
        }
    }
}