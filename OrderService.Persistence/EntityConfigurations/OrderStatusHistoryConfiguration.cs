using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Persistence.EntityConfigurations
{
    public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OldStatus).IsRequired();
            builder.Property(x => x.NewStatus).IsRequired();
            builder.Property(x => x.ChangedAt).IsRequired();
        }
    }
}
