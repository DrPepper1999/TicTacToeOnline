using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.RoomAggregate.Enums;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;

namespace TicTacToeOnline.Infrastructure.Persistence.Configurations
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            ConfigureRoomsTable(builder);
            ConfigureRoomTeamIdsTable(builder);
            ConfigureRoomPlayerIdsTable(builder);
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

            builder.OwnsOne(r => r.GameSetting);
        }

        private void ConfigureRoomTeamIdsTable(EntityTypeBuilder<Room> builder)
        {
            builder.OwnsMany(r => r.TeamIds, tb =>
            {
                tb.ToTable("RoomTeamIds");

                tb.WithOwner().HasForeignKey("RoomId");

                tb.HasKey("Id");

                tb.Property(t => t.Value)
                    .HasColumnName("TeamId")
                    .ValueGeneratedNever();
            });
            builder.Metadata.FindNavigation(nameof(Room.TeamIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureRoomPlayerIdsTable(EntityTypeBuilder<Room> builder)
        {
            builder.OwnsMany(m => m.PlayerIds, dib =>
            {
                dib.ToTable("RoomPlayerIds");

                dib.WithOwner().HasForeignKey("RoomId");

                dib.HasKey("Id");

                dib.Property(d => d.Value)
                    .HasColumnName("PlayerId")
                    .ValueGeneratedNever();
            });

            builder.Metadata.FindNavigation(nameof(Room.PlayerIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
