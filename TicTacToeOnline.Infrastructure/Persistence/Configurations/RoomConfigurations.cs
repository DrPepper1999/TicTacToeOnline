using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToeOnline.Domain.GameAggregate.Entities;
using TicTacToeOnline.Domain.GameAggregate.Enums;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.PlayerAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;
using TicTacToeOnline.Infrastructure.Persistence.Common.Conversion;

namespace TicTacToeOnline.Infrastructure.Persistence.Configurations
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            ConfigureRoomsTable(builder);
            ConfigurePlayersIdsTable(builder);
            ConfigureGameTable(builder);
        }

        private void ConfigureRoomsTable(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => RoomId.Create(value));

            builder.Property(r => r.Name)
                .HasMaxLength(100);

            builder.Property(r => r.Status)
                .HasConversion(status => status.ToString(),
                    str =>
                        (RoomStatus)Enum.Parse(typeof(RoomStatus), str));

            builder.Property(r => r.Password)
                .HasMaxLength(16);


        }

        private void ConfigureGameTable(EntityTypeBuilder<Room> builder)
        {
            builder.OwnsOne(r => r.Game, gb =>
            {
                gb.ToTable("Game");

                gb.WithOwner().HasForeignKey("RoomId");

                gb.HasKey("Id", "RoomId");

                gb.Property(g => g.Id)
                    .HasColumnName("GameId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => GameId.Create(value));

                gb.Property(g => g.PlayerTurn)
                    .HasConversion(
                        id => id.Value,
                        value => PlayerId.Create(value));

                gb.OwnsOne(g => g.Map, mb =>
                {
                    mb.ToTable("Map");

                    mb.WithOwner().HasForeignKey("GameId", "RoomId");

                    mb.HasKey(nameof(Map.Id), "GameId", "RoomId");

                    mb.Property(m => m.Id)
                        .HasColumnName("MapId")
                        .ValueGeneratedNever()
                        .HasConversion(
                            id => id.Value,
                            value => MapId.Create(value));

                    mb.Property(m => m.Size)
                        .HasMaxLength(16);

                    mb.Property("_fields")
                        .HasConversion<MapConverter>();
                });
            });
        }

        private void ConfigurePlayersIdsTable(EntityTypeBuilder<Room> builder)
        {
            builder.OwnsMany(r => r.PlayerIds, pib =>
            {
                pib.ToTable("PlayerIds");

                pib.WithOwner().HasForeignKey("RoomId");

                pib.HasKey("Id");

                pib.Property(p => p.Value)
                    .HasColumnName("PlayerId")
                    .ValueGeneratedNever();
            });


            builder.Metadata.FindNavigation(nameof(Room.PlayerIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
