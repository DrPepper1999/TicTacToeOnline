using MediatR;
using Microsoft.EntityFrameworkCore;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.GameAggregate;
using TicTacToeOnline.Domain.PlayerAggregate;
using TicTacToeOnline.Domain.RoomAggregate;
using TicTacToeOnline.Domain.TeamAggregate;
using TicTacToeOnline.Domain.UserAggregate;
using TicTacToeOnline.Infrastructure.Extension;
using TicTacToeOnline.Infrastructure.Persistence.Configurations;
using TicTacToeOnline.Infrastructure.Persistence.Outbox;

namespace TicTacToeOnline.Infrastructure.Persistence
{
    public class TicTacToeOnlineDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        private TicTacToeOnlineDbContext(DbContextOptions<TicTacToeOnlineDbContext> options)
            : base(options)
        {
        }

        public TicTacToeOnlineDbContext(DbContextOptions<TicTacToeOnlineDbContext> options,
            IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<OutboxMessage> OutBoxMessages { get; set; } = null!;
        public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);


            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed throught the DbContext will be commited
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

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
