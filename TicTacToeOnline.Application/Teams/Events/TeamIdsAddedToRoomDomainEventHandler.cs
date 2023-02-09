using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.Common.Enums;
using TicTacToeOnline.Domain.DomainEvents;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Teams.Events
{
    public class TeamIdsAddedToRoomDomainEventHandler : INotificationHandler<TeamIdsAddedToRoomDomainEvent>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ITeamRepository _teamRepository;

        public TeamIdsAddedToRoomDomainEventHandler(IRoomRepository roomRepository, ITeamRepository teamRepository)
        {
            _roomRepository = roomRepository;
            _teamRepository = teamRepository;
        }

        public async Task Handle(TeamIdsAddedToRoomDomainEvent notification, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(notification.RoomId.Value, cancellationToken);

            if (room is null)
            {
                return;
            }

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
