using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Models;
using TicTacToeOnline.Domain.GameAggregate;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.TeamAggregate;
using TicTacToeOnline.Domain.UserAggregate;
using TicTacToeOnline.Infrastructure.Persistence.Configurations;
using TicTacToeOnline.Infrastructure.Persistence.Outbox;

namespace TicTacToeOnline.Infrastructure.Persistence
{
    public class TicTacToeOnlineDbContext : DbContext, IUnitOfWork
    {
        public TicTacToeOnlineDbContext(DbContextOptions<TicTacToeOnlineDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<OutboxMessage> OutBoxMessages { get; set; } = null!;
        public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder
                //.ApplyConfigurationsFromAssembly(GetType().Assembly)

            builder.ApplyConfiguration(new UserConfigurations());
            builder.ApplyConfiguration(new PlayerConfigurations());
            builder.ApplyConfiguration(new TeamConfigurations());
            builder.ApplyConfiguration(new RoomConfigurations());
            builder.ApplyConfiguration(new GameConfigurations());
            builder.ApplyConfiguration(new OutboxMessageConfiguration());
            builder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
