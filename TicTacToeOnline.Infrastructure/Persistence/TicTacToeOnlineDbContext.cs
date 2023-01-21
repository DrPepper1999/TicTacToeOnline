using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Domain.RoomAggregate;

namespace TicTacToeOnline.Infrastructure.Persistence
{
    public class TicTacToeOnlineDbContext : DbContext
    {
        public TicTacToeOnlineDbContext(DbContextOptions<TicTacToeOnlineDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(TicTacToeOnlineDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
