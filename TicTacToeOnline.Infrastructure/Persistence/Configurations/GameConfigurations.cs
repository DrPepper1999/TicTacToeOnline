using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.GameAggregate;
using TicTacToeOnline.Domain.GameAggregate.Entities;
using TicTacToeOnline.Domain.GameAggregate.ValueObjects;
using TicTacToeOnline.Domain.RoomAggregate.ValueObjects;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;
using TicTacToeOnline.Infrastructure.Persistence.Common.Conversion;

namespace TicTacToeOnline.Infrastructure.Persistence.Configurations
{
    public class GameConfigurations : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            ConfigureGameTeamIdsTable(builder);
            ConfigureGamesTable(builder);
            ConfigureMapsTable(builder);
        }

        private void ConfigureGamesTable(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => GameId.Create(value));

            builder.Property(g => g.PlayerTurn)
                .HasConversion(
                    id => id.Value,
                    value => TeamId.Create(value));

            builder.Property(r => r.RoomId)
                .HasConversion(
                    id => id.Value,
                    value => RoomId.Create(value));

            builder.Property<Mark>("_currentMarkMove")
                .HasConversion(
                    mark => mark.ToString(),
                    str => (Mark)Enum.Parse(typeof(Mark), str));
        }

        private void ConfigureMapsTable(EntityTypeBuilder<Game> builder)
        {
            builder.OwnsOne(g => g.Map, mb =>
            {
                mb.ToTable("Map");

                mb.WithOwner().HasForeignKey("GameId");

                mb.HasKey(nameof(Map.Id), "GameId");

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

                mb.Property("_fillCellCount");
            });
        }

        private void ConfigureGameTeamIdsTable(EntityTypeBuilder<Game> builder)
        {
            builder.OwnsMany(g => g.TeamIds, dib =>
            {
                dib.ToTable("GameTeamIds");

                dib.WithOwner().HasForeignKey("GameId");

                dib.Property<int>("Id");

                dib.HasKey("Id");

                dib.Property(d => d.Value)
                    .HasColumnName("TeamId")
                    .ValueGeneratedNever();
            });

            builder.Metadata.FindNavigation(nameof(Game.TeamIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
