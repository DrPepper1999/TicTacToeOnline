using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToeOnline.Infrastructure.Persistence.Constants;
using TicTacToeOnline.Infrastructure.Persistence.Outbox;

namespace TicTacToeOnline.Infrastructure.Persistence.Configurations
{
    public class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
    {
        public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
        {
            builder.ToTable(TableNames.OutboxMessageConsumers);

            builder.HasKey(outboxMessageConsumer => new
            {
                outboxMessageConsumer.Id,
                outboxMessageConsumer.Name
            });
        }
    }
}
