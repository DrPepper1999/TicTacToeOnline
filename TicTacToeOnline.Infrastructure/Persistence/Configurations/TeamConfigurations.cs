using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.TeamAggregate;
using TicTacToeOnline.Domain.TeamAggregate.ValueObjects;

namespace TicTacToeOnline.Infrastructure.Persistence.Configurations
{
    public class TeamConfigurations : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            ConfigureTeamsTable(builder);
            ConfigureTeamPlayerIdsTable(builder);
        }

        private void ConfigureTeamsTable(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => TeamId.Create(value));

            builder.Property(t => t.Mark)
                .HasConversion(mark => mark.ToString(),
                    str =>
                        (Mark)Enum.Parse(typeof(Mark), str));

        }

        private void ConfigureTeamPlayerIdsTable(EntityTypeBuilder<Team> builder)
        {
            builder.OwnsMany(t => t.PlayerIds, pib =>
            {
                pib.ToTable("TeamPlayerIds");

                pib.WithOwner().HasForeignKey("TeamId");

                pib.Property<int>("Id");

                pib.HasKey("Id");

                pib.Property(d => d.Value)
                    .HasColumnName("PlayerId")
                    .ValueGeneratedNever();
            });

            builder.Metadata.FindNavigation(nameof(Team.PlayerIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
