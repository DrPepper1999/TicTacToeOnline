using Quartz;
using Newtonsoft.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using TicTacToeOnline.Domain.Common.Interfaces;
using TicTacToeOnline.Infrastructure.Persistence;
using TicTacToeOnline.Infrastructure.Persistence.Outbox;

namespace TicTacToeOnline.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutMessagesJob : IJob
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };

        private readonly TicTacToeOnlineDbContext _dbContext;
        private readonly IPublisher _publisher;

        public ProcessOutMessagesJob(TicTacToeOnlineDbContext dbContext, IPublisher publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _dbContext
                .Set<OutboxMessage>()
                .Where(m => m.ProcessedOnUtc == null &&
                            m.Error == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);

            foreach (var outboxMessage in messages)
            {
                var domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(outboxMessage.Content, JsonSerializerSettings);

                if (domainEvent == null)
                {
                    continue; // TODO отладка или ведение журнала
                }

                AsyncRetryPolicy policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(
                        3,
                        attempt => TimeSpan.FromMilliseconds(50 * attempt));

                PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>
                    _publisher.Publish(
                        domainEvent,
                        context.CancellationToken));

                outboxMessage.Error = result.FinalException?.ToString();
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}

