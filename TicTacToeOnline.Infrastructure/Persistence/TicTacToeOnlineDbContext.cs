using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.UserAggregate;
using TicTacToeOnline.Infrastructure.Persistence.Outbox;

namespace TicTacToeOnline.Infrastructure.Persistence
{
    public class TicTacToeOnlineDbContext : DbContext, IUnitOfWork
    {
        public TicTacToeOnlineDbContext(DbContextOptions<TicTacToeOnlineDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<OutboxMessage> OutBoxMessages { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(TicTacToeOnlineDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
