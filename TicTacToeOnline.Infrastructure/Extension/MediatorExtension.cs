using MediatR;
using TicTacToeOnline.Domain.Common.Interfaces;
using TicTacToeOnline.Infrastructure.Persistence;

namespace TicTacToeOnline.Infrastructure.Extension
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator,
            TicTacToeOnlineDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<IAggregateRoot>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .Select(x => x.Entity)
                .SelectMany(aggregateRoot =>
                {
                    var domainEvents = aggregateRoot.DomainEvents;

                    aggregateRoot.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
