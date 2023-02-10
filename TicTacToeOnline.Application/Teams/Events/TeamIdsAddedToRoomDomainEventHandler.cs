using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.DomainEvents;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Teams.Events
{
    public class TeamIdsAddedToRoomDomainEventHandler : INotificationHandler<TeamIdsAddedToRoomDomainEvent>
    {
        private readonly ITeamRepository _teamRepository;

        public TeamIdsAddedToRoomDomainEventHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task Handle(TeamIdsAddedToRoomDomainEvent notification, CancellationToken cancellationToken)
        {
            var mark = (Mark)1;
            foreach (var teamId in notification.TeamIds)
            {
                var team = Team.Create(mark, teamId); // TODO ставить разные Mark возможно с помощью умного enum

                await _teamRepository.AddAsync(team, cancellationToken);

                mark++;
            }

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
