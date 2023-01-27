using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.UserAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.Enums;

namespace TicTacToeOnline.Infrastructure.Persistence.Configurations
{
    public class PlayerConfigurations : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            ConfigurePlayerTable(builder);
            ConfigureConnectionInfoTable(builder);
        }

        private void ConfigurePlayerTable(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => PlayerId.Create(value));

            builder.Property(p => p.Name)
                .HasMaxLength(100);

            builder.Property(p => p.Mark)
                .HasConversion(
                    mark => mark.ToString(),
                    str => (Mark)Enum.Parse(typeof(Mark), str));

            builder.OwnsOne(p => p.AverageRating);

            builder.Property(p => p.UserId)
                .HasConversion(
                    id => TryGetUserId(id),
                    value => TrySetUserId(value));
        }

        private static Guid? TryGetUserId(UserId? userId) => userId?.Value ?? null;
        private static UserId? TrySetUserId(Guid? id) => id.HasValue ? UserId.Create(id.Value) : null;

        private void ConfigureConnectionInfoTable(EntityTypeBuilder<Player> builder)
        {
            builder.OwnsMany(p => p.Connections, cb =>
            {
                cb.ToTable("ConnectionInfo");

                cb.WithOwner().HasForeignKey("PlayerId");

                cb.HasKey("Id", "PlayerId");
            });

            builder.Metadata.FindNavigation(nameof(Player.Connections))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
