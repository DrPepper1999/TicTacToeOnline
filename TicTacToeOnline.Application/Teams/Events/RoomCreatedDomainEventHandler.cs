using MediatR;
using TicTacToeOnline.Application.Common.Interfaces.Persistence;
using TicTacToeOnline.Domain.DomainEvents;
using TicTacToeOnline.Domain.TeamAggregate;

namespace TicTacToeOnline.Application.Teams.Events
{
    public class RoomCreatedDomainEventHandler : INotificationHandler<RoomCreatedDomainEvent>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ISender _mediator;

        public RoomCreatedDomainEventHandler(IRoomRepository roomRepository, ITeamRepository teamRepository, ISender mediator)
        {
            _roomRepository = roomRepository;
            _teamRepository = teamRepository;
            _mediator = mediator;
        }

        public async Task Handle(RoomCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetByIdAsync(notification.RoomId.Value, cancellationToken);

            if (room is null)
            {
                return;
            }

            for (var i = 0; i < room.GameSetting.TeamCount; i++)
            {
                var team = Team.Create();

                room.AddTeamId(team.Id);

                await _teamRepository.AddAsync(team, cancellationToken);
            }

            await _roomRepository.UpdateAsync(room, cancellationToken);

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
